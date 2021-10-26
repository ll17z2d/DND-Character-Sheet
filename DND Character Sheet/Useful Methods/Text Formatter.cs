using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DND_Character_Sheet.Useful_Methods
{
    public static class TextFormatter
    {
        public static string ListToString(List<string> list, bool separateSentences = false)
        {
            string output = "";
            foreach (var listItem in list)
            {
                var listItemFormatted = listItem;
                if (separateSentences)
                {
                    listItemFormatted = listItem.Replace(".", ".\n");

                }
                output = string.Concat(output, "\n", listItemFormatted);
            }

            return output;
        }

        public static string DictionaryToString(Dictionary<string, string> dictionary, bool separateSentences = false)
        {
            string output = "";
            foreach (var dictionaryItem in dictionary)
            {
                var dictionaryItemFormatted = $"{dictionaryItem.Key} - {dictionaryItem.Value}";
                if (separateSentences)
                {
                    dictionaryItemFormatted = dictionaryItemFormatted.Insert(dictionaryItemFormatted.ToCharArray().Length, "\n");

                }
                output = string.Concat(output, "\n", dictionaryItemFormatted);
            }

            return output;
        }

        public static string FormatList<T>(List<T> list, string property)
        {
            string output = "";
            for (int i = 0; i < list.Count; i++)
            {
                var listItem = GetListItemFromStringProperty(list, i, property).ToLower().Replace("-", " ");

                if (i == list.Count - 1)
                {
                    output = string.Concat(output, listItem);
                }
                else if (i == list.Count - 2)
                {
                    var tempConcat = string.Concat(listItem, " and ");
                    output = string.Concat(output, tempConcat);
                }
                else
                {
                    var tempConcat = string.Concat(listItem, ", ");
                    output = string.Concat(output, tempConcat);
                }
            }

            return output;
        }

        public static string ExtractFileNameFromPath(string filePath) 
            => filePath.Substring(filePath.LastIndexOf('\\') + 1, (filePath.LastIndexOf('.') - 1) - filePath.LastIndexOf("\\", StringComparison.Ordinal));

        private static string GetListItemFromStringProperty<T>(List<T> list, int index, string property)
            => (string)list[index].GetType().GetProperty(property)?.GetValue(list[index]);
    }
}
