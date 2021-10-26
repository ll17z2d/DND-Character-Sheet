using System;
using System.Collections.Generic;
using System.Text;
using DND_Character_Sheet.Useful_Methods;

namespace DND_Character_Sheet.Wrappers
{
    public interface ITextFormatterWrapper
    {
        public string ListToString(List<string> list, bool separateSentences = false);

        public string DictionaryToString(Dictionary<string, string> list, bool separateSentences = false);

        public string FormatList<T>(List<T> list, string property);

        public string ExtractFileNameFromPath(string filePath);
    }

    public class TextFormatterWrapper : ITextFormatterWrapper
    {
        public string ListToString(List<string> list, bool separateSentences = false) 
            => TextFormatter.ListToString(list, separateSentences);

        public string DictionaryToString(Dictionary<string, string> dictionary, bool separateSentences = false) 
            => TextFormatter.DictionaryToString(dictionary, separateSentences);

        public string FormatList<T>(List<T> list, string property) 
            => TextFormatter.FormatList(list, property);

        public string ExtractFileNameFromPath(string filePath) 
            => TextFormatter.ExtractFileNameFromPath(filePath);
    }
}
