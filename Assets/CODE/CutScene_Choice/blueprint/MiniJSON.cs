using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MiniJSON
{
    public static class Json
    {
        public static object Deserialize(string json)
        {
            if (json == null)
            {
                return null;
            }

            return Parser.Parse(json);
        }

        sealed class Parser : IDisposable
        {
            const string WORD_BREAK = "{}[],:\"";

            public static bool IsWordBreak(char c)
            {
                return Char.IsWhiteSpace(c) || WORD_BREAK.IndexOf(c) != -1;
            }

            enum TOKEN
            {
                NONE,
                CURLY_OPEN,
                CURLY_CLOSE,
                SQUARED_OPEN,
                SQUARED_CLOSE,
                COLON,
                COMMA,
                STRING,
                NUMBER,
                TRUE,
                FALSE,
                NULL
            };

            StringReader json;

            Parser(string jsonString)
            {
                json = new StringReader(jsonString);
            }

            public static object Parse(string jsonString)
            {
                using (var instance = new Parser(jsonString))
                {
                    return instance.ParseValue();
                }
            }

            public void Dispose()
            {
                json.Dispose();
                json = null;
            }

            Dictionary<string, object> ParseObject()
            {
                Dictionary<string, object> table = new Dictionary<string, object>();

                json.Read();

                while (true)
                {
                    TOKEN nextToken = NextToken();

                    if (nextToken == TOKEN.NONE)
                    {
                        return null;
                    }
                    else if (nextToken == TOKEN.CURLY_CLOSE)
                    {
                        return table;
                    }

                    string name = ParseString();
                    if (name == null)
                    {
                        return null;
                    }

                    if (NextToken() != TOKEN.COLON)
                    {
                        return null;
                    }

                    json.Read();

                    table[name] = ParseValue();
                }
            }

            List<object> ParseArray()
            {
                List<object> array = new List<object>();

                json.Read();

                while (true)
                {
                    TOKEN nextToken = NextToken();

                    if (nextToken == TOKEN.NONE)
                    {
                        return null;
                    }
                    else if (nextToken == TOKEN.SQUARED_CLOSE)
                    {
                        break;
                    }

                    array.Add(ParseValue());
                }

                return array;
            }

            object ParseValue()
            {
                TOKEN nextToken = NextToken();
                switch (nextToken)
                {
                    case TOKEN.STRING:
                        return ParseString();
                    case TOKEN.NUMBER:
                        return ParseNumber();
                    case TOKEN.CURLY_OPEN:
                        return ParseObject();
                    case TOKEN.SQUARED_OPEN:
                        return ParseArray();
                    case TOKEN.TRUE:
                        return true;
                    case TOKEN.FALSE:
                        return false;
                    case TOKEN.NULL:
                        return null;
                    default:
                        return null;
                }
            }

            string ParseString()
            {
                StringBuilder s = new StringBuilder();
                char c;

                json.Read();

                bool parsing = true;
                while (parsing)
                {
                    if (json.Peek() == -1)
                    {
                        break;
                    }

                    c = NextChar();
                    switch (c)
                    {
                        case '"':
                            parsing = false;
                            break;
                        case '\\':
                            if (json.Peek() == -1)
                            {
                                parsing = false;
                                break;
                            }

                            c = NextChar();
                            switch (c)
                            {
                                case '"':
                                case '\\':
                                case '/':
                                    s.Append(c);
                                    break;
                                case 'b':
                                    s.Append('\b');
                                    break;
                                case 'f':
                                    s.Append('\f');
                                    break;
                                case 'n':
                                    s.Append('\n');
                                    break;
                                case 'r':
                                    s.Append('\r');
                                    break;
                                case 't':
                                    s.Append('\t');
                                    break;
                                case 'u':
                                    var hex = new char[4];

                                    for (int i = 0; i < 4; i++)
                                    {
                                        hex[i] = NextChar();
                                    }

                                    s.Append((char)Convert.ToInt32(new string(hex), 16));
                                    break;
                            }
                            break;
                        default:
                            s.Append(c);
                            break;
                    }
                }

                return s.ToString();
            }

            object ParseNumber()
            {
                string number = NextWord();

                if (number.IndexOf('.') == -1)
                {
                    long parsedInt;
                    Int64.TryParse(number, out parsedInt);
                    return parsedInt;
                }

                double parsedDouble;
                Double.TryParse(number, out parsedDouble);
                return parsedDouble;
            }

            void EatWhitespace()
            {
                while (Char.IsWhiteSpace(PeekChar()))
                {
                    json.Read();

                    if (json.Peek() == -1)
                    {
                        break;
                    }
                }
            }

            char PeekChar()
            {
                return Convert.ToChar(json.Peek());
            }

            char NextChar()
            {
                return Convert.ToChar(json.Read());
            }

            string NextWord()
            {
                StringBuilder word = new StringBuilder();

                while (!IsWordBreak(PeekChar()))
                {
                    word.Append(NextChar());

                    if (json.Peek() == -1)
                    {
                        break;
                    }
                }

                return word.ToString();
            }

            TOKEN NextToken()
            {
                EatWhitespace();

                if (json.Peek() == -1)
                {
                    return TOKEN.NONE;
                }

                char c = PeekChar();
                switch (c)
                {
                    case '{':
                        return TOKEN.CURLY_OPEN;
                    case '}':
                        json.Read();
                        return TOKEN.CURLY_CLOSE;
                    case '[':
                        return TOKEN.SQUARED_OPEN;
                    case ']':
                        json.Read();
                        return TOKEN.SQUARED_CLOSE;
                    case ',':
                        json.Read();
                        return TOKEN.COMMA;
                    case '"':
                        return TOKEN.STRING;
                    case ':':
                        return TOKEN.COLON;
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case '-':
                        return TOKEN.NUMBER;
                    case 'f':
                        if (NextWord() == "false")
                        {
                            return TOKEN.FALSE;
                        }
                        break;
                    case 't':
                        if (NextWord() == "true")
                        {
                            return TOKEN.TRUE;
                        }
                        break;
                    case 'n':
                        if (NextWord() == "null")
                        {
                            return TOKEN.NULL;
                        }
                        break;
                }

                return TOKEN.NONE;
            }
        }
    }
}
