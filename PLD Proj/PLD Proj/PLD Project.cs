
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using System.Windows.Forms;


namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF          =  0, // (EOF)
        SYMBOL_ERROR        =  1, // (Error)
        SYMBOL_WHITESPACE   =  2, // Whitespace
        SYMBOL_MINUS        =  3, // '-'
        SYMBOL_MINUSMINUS   =  4, // '--'
        SYMBOL_EXCLAMEQ     =  5, // '!='
        SYMBOL_PERCENT      =  6, // '%'
        SYMBOL_LPAREN       =  7, // '('
        SYMBOL_RPAREN       =  8, // ')'
        SYMBOL_TIMES        =  9, // '*'
        SYMBOL_TIMESTIMES   = 10, // '**'
        SYMBOL_COMMA        = 11, // ','
        SYMBOL_DIV          = 12, // '/'
        SYMBOL_COLON        = 13, // ':'
        SYMBOL_SEMI         = 14, // ';'
        SYMBOL_LBRACE       = 15, // '{'
        SYMBOL_RBRACE       = 16, // '}'
        SYMBOL_PLUS         = 17, // '+'
        SYMBOL_PLUSPLUS     = 18, // '++'
        SYMBOL_LT           = 19, // '<'
        SYMBOL_LTEQ         = 20, // '<='
        SYMBOL_EQEQ         = 21, // '=='
        SYMBOL_GT           = 22, // '>'
        SYMBOL_GTEQ         = 23, // '>='
        SYMBOL_BEGIN        = 24, // Begin
        SYMBOL_BREAK        = 25, // break
        SYMBOL_DECIMAL      = 26, // Decimal
        SYMBOL_DEFINE       = 27, // Define
        SYMBOL_DEFUALT      = 28, // defualt
        SYMBOL_DIGIT        = 29, // Digit
        SYMBOL_ELSE         = 30, // else
        SYMBOL_ENDIF        = 31, // EndIf
        SYMBOL_ENDSTMT      = 32, // Endstmt
        SYMBOL_EQUAL        = 33, // Equal
        SYMBOL_FINISH       = 34, // Finish
        SYMBOL_FOR          = 35, // For
        SYMBOL_ID           = 36, // Id
        SYMBOL_IF           = 37, // if
        SYMBOL_LET          = 38, // Let
        SYMBOL_NUMBER       = 39, // Number
        SYMBOL_OPTION       = 40, // Option
        SYMBOL_SWITCH       = 41, // Switch
        SYMBOL_TEXT         = 42, // Text
        SYMBOL_THEN         = 43, // Then
        SYMBOL_VOID         = 44, // void
        SYMBOL_ASSIGN       = 45, // <assign>
        SYMBOL_CASE_ITEM    = 46, // <case_item>
        SYMBOL_CASE_LIST    = 47, // <case_list>
        SYMBOL_CONCEPT      = 48, // <concept>
        SYMBOL_COND         = 49, // <cond>
        SYMBOL_DATA         = 50, // <data>
        SYMBOL_DEFUALT_CASE = 51, // <defualt_case>
        SYMBOL_DIGIT2       = 52, // <digit>
        SYMBOL_EXP          = 53, // <exp>
        SYMBOL_EXPR         = 54, // <expr>
        SYMBOL_FACTOR       = 55, // <factor>
        SYMBOL_FOR_STMT     = 56, // <for_stmt>
        SYMBOL_ID2          = 57, // <id>
        SYMBOL_IF_STMT      = 58, // <if_stmt>
        SYMBOL_METHOD_CALL  = 59, // <method_call>
        SYMBOL_METHOD_DECL  = 60, // <method_decl>
        SYMBOL_OP           = 61, // <op>
        SYMBOL_PARAM        = 62, // <param>
        SYMBOL_PARAMS       = 63, // <params>
        SYMBOL_PROGRAM      = 64, // <program>
        SYMBOL_RETURNTYPE   = 65, // <returnType>
        SYMBOL_STEP         = 66, // <step>
        SYMBOL_STMT_LIST    = 67, // <stmt_list>
        SYMBOL_SWITCH_CASE  = 68, // <switch_case>
        SYMBOL_TERM         = 69  // <term>
    };

    enum RuleConstants : int
    {
        RULE_PROGRAM_BEGIN_FINISH                               =  0, // <program> ::= Begin <stmt_list> Finish
        RULE_STMT_LIST                                          =  1, // <stmt_list> ::= <concept>
        RULE_STMT_LIST_PLUS                                     =  2, // <stmt_list> ::= <concept> '+' <stmt_list>
        RULE_CONCEPT                                            =  3, // <concept> ::= <assign>
        RULE_CONCEPT2                                           =  4, // <concept> ::= <if_stmt>
        RULE_CONCEPT3                                           =  5, // <concept> ::= <for_stmt>
        RULE_CONCEPT4                                           =  6, // <concept> ::= <switch_case>
        RULE_CONCEPT5                                           =  7, // <concept> ::= <method_decl>
        RULE_CONCEPT6                                           =  8, // <concept> ::= <method_call>
        RULE_ASSIGN_LET_EQUAL_ENDSTMT                           =  9, // <assign> ::= Let <id> Equal <expr> Endstmt
        RULE_ID_ID                                              = 10, // <id> ::= Id
        RULE_EXPR_PLUS                                          = 11, // <expr> ::= <expr> '+' <term>
        RULE_EXPR_MINUS                                         = 12, // <expr> ::= <expr> '-' <term>
        RULE_EXPR                                               = 13, // <expr> ::= <term>
        RULE_TERM_TIMES                                         = 14, // <term> ::= <term> '*' <factor>
        RULE_TERM_DIV                                           = 15, // <term> ::= <term> '/' <factor>
        RULE_TERM_PERCENT                                       = 16, // <term> ::= <term> '%' <factor>
        RULE_TERM                                               = 17, // <term> ::= <factor>
        RULE_FACTOR_TIMESTIMES                                  = 18, // <factor> ::= <factor> '**' <exp>
        RULE_FACTOR                                             = 19, // <factor> ::= <exp>
        RULE_EXP_LPAREN_RPAREN                                  = 20, // <exp> ::= '(' <expr> ')'
        RULE_EXP                                                = 21, // <exp> ::= <id>
        RULE_EXP2                                               = 22, // <exp> ::= <digit>
        RULE_DIGIT_DIGIT                                        = 23, // <digit> ::= Digit
        RULE_IF_STMT_IF_LPAREN_RPAREN_THEN_ENDIF                = 24, // <if_stmt> ::= if '(' <cond> ')' Then <stmt_list> EndIf
        RULE_IF_STMT_IF_LPAREN_RPAREN_THEN_ELSE_ENDIF           = 25, // <if_stmt> ::= if '(' <cond> ')' Then <stmt_list> else <stmt_list> EndIf
        RULE_COND                                               = 26, // <cond> ::= <expr> <op> <expr>
        RULE_OP_LT                                              = 27, // <op> ::= '<'
        RULE_OP_GT                                              = 28, // <op> ::= '>'
        RULE_OP_EQEQ                                            = 29, // <op> ::= '=='
        RULE_OP_EXCLAMEQ                                        = 30, // <op> ::= '!='
        RULE_OP_LTEQ                                            = 31, // <op> ::= '<='
        RULE_OP_GTEQ                                            = 32, // <op> ::= '>='
        RULE_SWITCH_CASE_SWITCH_LPAREN_RPAREN_LBRACE_RBRACE     = 33, // <switch_case> ::= Switch '(' <expr> ')' '{' <case_list> '}'
        RULE_CASE_LIST                                          = 34, // <case_list> ::= <case_item>
        RULE_CASE_LIST_PLUS                                     = 35, // <case_list> ::= <case_item> '+' <defualt_case>
        RULE_CASE_ITEM_OPTION_COLON_BREAK_SEMI                  = 36, // <case_item> ::= Option <expr> ':' <stmt_list> break ';'
        RULE_DEFUALT_CASE_DEFUALT_COLON                         = 37, // <defualt_case> ::= defualt ':' <stmt_list>
        RULE_FOR_STMT_FOR_LPAREN_SEMI_SEMI_RPAREN_LBRACE_RBRACE = 38, // <for_stmt> ::= For '(' <data> <assign> ';' <cond> ';' <step> ')' '{' <stmt_list> '}'
        RULE_DATA_NUMBER                                        = 39, // <data> ::= Number
        RULE_DATA_DECIMAL                                       = 40, // <data> ::= Decimal
        RULE_DATA_TEXT                                          = 41, // <data> ::= Text
        RULE_STEP_PLUSPLUS                                      = 42, // <step> ::= <id> '++'
        RULE_STEP_MINUSMINUS                                    = 43, // <step> ::= <id> '--'
        RULE_STEP_PLUSPLUS2                                     = 44, // <step> ::= '++' <id>
        RULE_STEP_MINUSMINUS2                                   = 45, // <step> ::= '--' <id>
        RULE_STEP                                               = 46, // <step> ::= <assign>
        RULE_METHOD_DECL_DEFINE_LPAREN_RPAREN_LBRACE_RBRACE     = 47, // <method_decl> ::= Define <returnType> <id> '(' <params> ')' '{' <stmt_list> '}'
        RULE_RETURNTYPE_NUMBER                                  = 48, // <returnType> ::= Number
        RULE_RETURNTYPE_DECIMAL                                 = 49, // <returnType> ::= Decimal
        RULE_RETURNTYPE_TEXT                                    = 50, // <returnType> ::= Text
        RULE_RETURNTYPE_VOID                                    = 51, // <returnType> ::= void
        RULE_PARAMS                                             = 52, // <params> ::= <param>
        RULE_PARAMS_COMMA                                       = 53, // <params> ::= <param> ',' <params>
        RULE_PARAM                                              = 54, // <param> ::= <data> <id>
        RULE_METHOD_CALL_LPAREN_RPAREN_ENDSTMT                  = 55  // <method_call> ::= <id> '(' <expr> ')' Endstmt
    };

    public class MyParser
    {
        private LALRParser parser;
        ListBox lst;
        public MyParser(string filename,ListBox lst)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
            this.lst = lst;
            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
        }

        public void Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                //todo: Use your object any way you like
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUSMINUS :
                //'--'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMEQ :
                //'!='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PERCENT :
                //'%'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMESTIMES :
                //'**'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMA :
                //','
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COLON :
                //':'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SEMI :
                //';'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACE :
                //'{'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACE :
                //'}'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUSPLUS :
                //'++'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LTEQ :
                //'<='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQEQ :
                //'=='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GTEQ :
                //'>='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BEGIN :
                //Begin
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BREAK :
                //break
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DECIMAL :
                //Decimal
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DEFINE :
                //Define
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DEFUALT :
                //defualt
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT :
                //Digit
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //else
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ENDIF :
                //EndIf
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ENDSTMT :
                //Endstmt
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQUAL :
                //Equal
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FINISH :
                //Finish
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR :
                //For
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID :
                //Id
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LET :
                //Let
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NUMBER :
                //Number
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OPTION :
                //Option
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH :
                //Switch
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TEXT :
                //Text
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_THEN :
                //Then
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VOID :
                //void
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGN :
                //<assign>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE_ITEM :
                //<case_item>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE_LIST :
                //<case_list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONCEPT :
                //<concept>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COND :
                //<cond>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DATA :
                //<data>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DEFUALT_CASE :
                //<defualt_case>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT2 :
                //<digit>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXP :
                //<exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPR :
                //<expr>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FACTOR :
                //<factor>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR_STMT :
                //<for_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID2 :
                //<id>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF_STMT :
                //<if_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_METHOD_CALL :
                //<method_call>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_METHOD_DECL :
                //<method_decl>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OP :
                //<op>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PARAM :
                //<param>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PARAMS :
                //<params>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM :
                //<program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RETURNTYPE :
                //<returnType>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STEP :
                //<step>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STMT_LIST :
                //<stmt_list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH_CASE :
                //<switch_case>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TERM :
                //<term>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_PROGRAM_BEGIN_FINISH :
                //<program> ::= Begin <stmt_list> Finish
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_LIST :
                //<stmt_list> ::= <concept>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_LIST_PLUS :
                //<stmt_list> ::= <concept> '+' <stmt_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT :
                //<concept> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT2 :
                //<concept> ::= <if_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT3 :
                //<concept> ::= <for_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT4 :
                //<concept> ::= <switch_case>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT5 :
                //<concept> ::= <method_decl>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT6 :
                //<concept> ::= <method_call>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGN_LET_EQUAL_ENDSTMT :
                //<assign> ::= Let <id> Equal <expr> Endstmt
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ID_ID :
                //<id> ::= Id
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_PLUS :
                //<expr> ::= <expr> '+' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_MINUS :
                //<expr> ::= <expr> '-' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR :
                //<expr> ::= <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_TIMES :
                //<term> ::= <term> '*' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_DIV :
                //<term> ::= <term> '/' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_PERCENT :
                //<term> ::= <term> '%' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM :
                //<term> ::= <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_TIMESTIMES :
                //<factor> ::= <factor> '**' <exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR :
                //<factor> ::= <exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_LPAREN_RPAREN :
                //<exp> ::= '(' <expr> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP :
                //<exp> ::= <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP2 :
                //<exp> ::= <digit>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DIGIT_DIGIT :
                //<digit> ::= Digit
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STMT_IF_LPAREN_RPAREN_THEN_ENDIF :
                //<if_stmt> ::= if '(' <cond> ')' Then <stmt_list> EndIf
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STMT_IF_LPAREN_RPAREN_THEN_ELSE_ENDIF :
                //<if_stmt> ::= if '(' <cond> ')' Then <stmt_list> else <stmt_list> EndIf
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COND :
                //<cond> ::= <expr> <op> <expr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_LT :
                //<op> ::= '<'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_GT :
                //<op> ::= '>'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EQEQ :
                //<op> ::= '=='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EXCLAMEQ :
                //<op> ::= '!='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_LTEQ :
                //<op> ::= '<='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_GTEQ :
                //<op> ::= '>='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SWITCH_CASE_SWITCH_LPAREN_RPAREN_LBRACE_RBRACE :
                //<switch_case> ::= Switch '(' <expr> ')' '{' <case_list> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASE_LIST :
                //<case_list> ::= <case_item>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASE_LIST_PLUS :
                //<case_list> ::= <case_item> '+' <defualt_case>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASE_ITEM_OPTION_COLON_BREAK_SEMI :
                //<case_item> ::= Option <expr> ':' <stmt_list> break ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DEFUALT_CASE_DEFUALT_COLON :
                //<defualt_case> ::= defualt ':' <stmt_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_STMT_FOR_LPAREN_SEMI_SEMI_RPAREN_LBRACE_RBRACE :
                //<for_stmt> ::= For '(' <data> <assign> ';' <cond> ';' <step> ')' '{' <stmt_list> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_NUMBER :
                //<data> ::= Number
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_DECIMAL :
                //<data> ::= Decimal
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_TEXT :
                //<data> ::= Text
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_PLUSPLUS :
                //<step> ::= <id> '++'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_MINUSMINUS :
                //<step> ::= <id> '--'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_PLUSPLUS2 :
                //<step> ::= '++' <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_MINUSMINUS2 :
                //<step> ::= '--' <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP :
                //<step> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_METHOD_DECL_DEFINE_LPAREN_RPAREN_LBRACE_RBRACE :
                //<method_decl> ::= Define <returnType> <id> '(' <params> ')' '{' <stmt_list> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_RETURNTYPE_NUMBER :
                //<returnType> ::= Number
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_RETURNTYPE_DECIMAL :
                //<returnType> ::= Decimal
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_RETURNTYPE_TEXT :
                //<returnType> ::= Text
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_RETURNTYPE_VOID :
                //<returnType> ::= void
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PARAMS :
                //<params> ::= <param>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PARAMS_COMMA :
                //<params> ::= <param> ',' <params>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PARAM :
                //<param> ::= <data> <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_METHOD_CALL_LPAREN_RPAREN_ENDSTMT :
                //<method_call> ::= <id> '(' <expr> ')' Endstmt
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+"in line"+args.UnexpectedToken.Location.LineNr;
            lst.Items.Add(message);
            string m2 = "Expected token: "+args.ExpectedTokens.ToString();
            lst.Items.Add(m2);
            //todo: Report message to UI?
        }

    }
}
