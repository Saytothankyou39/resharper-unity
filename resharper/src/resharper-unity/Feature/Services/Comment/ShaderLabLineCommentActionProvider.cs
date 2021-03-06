﻿using JetBrains.ReSharper.Feature.Services.Comment;
using JetBrains.ReSharper.Plugins.Unity.Psi.ShaderLab;
using JetBrains.ReSharper.Plugins.Unity.Psi.ShaderLab.Parsing;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Parsing;

namespace JetBrains.ReSharper.Plugins.Unity.Feature.Services.Comment
{
    [Language(typeof(ShaderLabLanguage))]
    public class ShaderLabLineCommentActionProvider : SimpleLineCommentActionProvider
    {
        protected override bool IsNewLine(TokenNodeType tokenType)
        {
            return tokenType == ShaderLabTokenType.END_OF_LINE_COMMENT;
        }

        protected override bool IsEndOfLineComment(TokenNodeType tokenType, string tokenText)
        {
            return tokenType.IsComment && tokenText.StartsWith("//");
        }

        protected override bool IsWhitespace(TokenNodeType tokenType)
        {
            return tokenType.IsWhitespace;
        }

        public override string StartLineCommentMarker => "//";
    }
}