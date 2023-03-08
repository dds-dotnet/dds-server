﻿using DDS.Net.Server.Entities;

namespace DDS.Net.Server.Core.Internal.IOProcessor.Types.Variable
{
    internal class CompoundVariable : BaseVariable
    {
        public List<BaseVariable> VariablesGroup { get; private set; } = new();

        public CompoundVariable(ushort id, string name) : base(id, name)
        {
            VariableType = VariableType.Compound;
        }
        /// <summary>
        /// Adds a variable to the composition / group.
        /// </summary>
        /// <param name="variable">The variable.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddVariable(BaseVariable variable)
        {
            if (variable == null) { throw new ArgumentNullException(nameof(variable)); }

            VariablesGroup.Add(variable);
        }

        protected override int GetTypeSizeOnBuffer()
        {
            throw new NotImplementedException();
        }

        protected override int GetValueSizeOnBuffer()
        {
            throw new NotImplementedException();
        }

        protected override void WriteTypeOnBuffer(ref byte[] buffer, ref int offset)
        {
            throw new NotImplementedException();
        }

        protected override void WriteValueOnBuffer(ref byte[] buffer, ref int offset)
        {
            throw new NotImplementedException();
        }
    }
}
