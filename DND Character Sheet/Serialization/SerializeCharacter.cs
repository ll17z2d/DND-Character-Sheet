using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.Wrappers;
//using iText.Kernel.Pdf;
using System.IO;

namespace DND_Character_Sheet.Serialization
{
    public static class SerializeCharacter
    {
        public static void SaveCharacterToFileJSON(ICharacterModel character, IJsonConvertWrapper jsonConvertWrapper, IFileOperationsWrapper fileOperationsWrapper) 
            => fileOperationsWrapper.JSONFileWriteAllText(character.FilePath, jsonConvertWrapper.SerializeObject(character));

        public static void SaveCharacterToFilePDF(ICharacterModel character, IJsonConvertWrapper jsonConvertWrapper, IFileOperationsWrapper fileOperationsWrapper)
        {
            //fileOperationsWrapper.PDFFileWriteAllText(character.FilePath, jsonConvertWrapper.SerializeObject(character));

            //using var memoryStream = new MemoryStream();
            //var pdfReader = new PdfReader(new MemoryStream(templateFileByteArray));
            //var pdfStamper = new PdfStamper(pdfReader, memoryStream, '\0', false);
            //var pdfFormFields = pdfStamper.AcroFields;
            //pdfFormFields.GenerateAppearances = true;
            //pdfFormFields.SetField("TextFullName", customer.Name, customer.Name);
            //pdfStamper.FormFlattening = true;
            //pdfReader.Close();
            //pdfStamper.Close();

            //string fileNameExisting = @"C:\Users\zakar\OneDrive\Documents\DnD Sheet Saves\PDF Testing\Official 5E Character Sheet.pdf";
            //string fileNameNew = @"C:\Users\zakar\OneDrive\Documents\DnD Sheet Saves\PDF Testing\Generated Official 5E Character Sheet.pdf";

            //using (var existingFileStream = new FileStream(fileNameExisting, FileMode.Open))
            //using (var newFileStream = new FileStream(fileNameNew, FileMode.Create))
            //{
            //    // Open existing PDF
            //    var pdfReader = new PdfReader(existingFileStream);

            //    // PdfStamper, which will create
            //    var stamper = new PdfStamper(pdfReader, newFileStream);

            //    var form = stamper.AcroFields;
            //    var fieldKeys = form.Fields.Keys;

            //    foreach (string fieldKey in fieldKeys)
            //    {
            //        form.SetField(fieldKey, "REPLACED!");
            //    }

            //    stamper.Close();
            //    pdfReader.Close();
            //}

            new SerializePDF().Serialize(character);
        }

        public static ICharacterModel OpenCharacterFromFileJSON(string filePath, IJsonConvertWrapper jsonConvertWrapper, IFileOperationsWrapper fileOperationsWrapper)
        {
            var character = jsonConvertWrapper.DeserializeObject<CharacterModel>(fileOperationsWrapper.FileReadAllText(filePath));

            foreach (var spellLevelViewModel in character.AllSpells)
                spellLevelViewModel.RemoveExtraDeserialisedSpells();

            return character;
        }

        public static ICharacterModel OpenCharacterFromFilePDF(string filePath, IJsonConvertWrapper jsonConvertWrapper, IFileOperationsWrapper fileOperationsWrapper)
        {
            return default(ICharacterModel);
        }
    }
}
