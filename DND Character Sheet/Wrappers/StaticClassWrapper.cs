using System;
using System.Collections.Generic;
using System.Text;

namespace DND_Character_Sheet.Wrappers
{
    public interface IStaticClassWrapper
    {
        public ITextFormatterWrapper TextFormatterWrapper { get; set; }

        public ISerializeCharacterWrapper SerializeCharacterWrapper { get; set; }
    }

    public class StaticClassWrapper : IStaticClassWrapper
    {
        public StaticClassWrapper(ITextFormatterWrapper textFormatterWrapper, ISerializeCharacterWrapper serializeCharacterWrapper)
        {
            TextFormatterWrapper = textFormatterWrapper;
            SerializeCharacterWrapper = serializeCharacterWrapper;
        }

        public ITextFormatterWrapper TextFormatterWrapper { get; set; }

        public ISerializeCharacterWrapper SerializeCharacterWrapper { get; set; }
    }
}
