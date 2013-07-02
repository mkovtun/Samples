open System
open System.IO

let Generate content = File.WriteAllText (Path.Combine(__SOURCE_DIRECTORY__, "..\MyProject\Exceptions.cs"), content)

let fileTemplate = "
using System;
using System.Runtime.Serialization;

namespace MyProject
{{
{0}
}}
"

let exceptionRecordTemplate = "
    [Serializable]
    public sealed class {0}: Exception
    {{
        private const string defaultMessage = \"{4}\";
        {1}
        public {0}()
        {{
        }}

        public {0}(string message): base(message)
        {{
        }}

        public {0}(string message, Exception innerException): base(message, innerException)
        {{
        }}

        public {0}({2}): base(defaultMessage)
        {{{3}
        }}

        public {0}({2}, Exception innerException): base(defaultMessage, innerException)
        {{{3}
        }}

        public {0}({2}, string message, Exception innerException): base(message, innerException)
        {{{3}
        }}
    }}
"

let propertyTempate = "
        public {1} {0}
        {{
            get;
            private set;
        }}
"

let ctorAssignment = "
            this.{0} = {0};"

type Argument =
    {
        Name: string
        Type: Type
    }   

let arg (argumentType:Type) (argumentName:string) = {Argument.Name = argumentName; Argument.Type = argumentType}
let exc (name:string) (args: Argument list) (defaultMessage:string) = 
    String.Format(exceptionRecordTemplate, 
                    name, 
                    args 
                    |> List.choose (fun x -> Some(String.Format(propertyTempate, x.Name, x.Type.ToString()))) 
                    |> List.reduce (fun x y -> x + y), 
                    args 
                    |> List.choose (fun x -> Some(String.Format("{1} {0}", x.Name, x.Type.Name))) 
                    |> List.reduce (fun x y -> String.Format("{0}, {1}", x, y)), 
                    args 
                    |> List.choose (fun x -> Some(String.Format(ctorAssignment, x.Name))) 
                    |> List.reduce (fun x y -> x + y), 
                    defaultMessage)


let guid name = arg typeof<Guid> name
let email = arg typeof<string> "Email"

let exceptions = 
                [
                    exc "EntityNotFoundException" [arg typeof<Type> "EntityType"; guid "EntityId"] "Specified entity was not found"
                    exc "InvalidEmailFormatException" [guid "UserId"; email] "Specified e-mail in not in a correct format"
                ]


Generate (String.Format(fileTemplate, List.reduce (fun x y -> x + y) exceptions))