namespace FuseBox

open FDOM.Core.Parsing
open FDOM.Rendering
open Microsoft.FSharp.Core

module Formatting =

    open FDOM.Core.Parsing
    
    [<RequireQualifiedAccess>]
    module MarkDown =
        
        let prepare (md: string) =
            md.Replace("\r\n", "\n").Split('\n')
            |> List.ofArray
        
        let toHtml (md: string) =
            
            let parser = prepare md |> Parser.ParseLines
            
            let blocks = parser.CreateBlockContent()
            
            Html.renderFromBlocks blocks