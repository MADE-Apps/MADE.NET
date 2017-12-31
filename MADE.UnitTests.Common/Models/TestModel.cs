namespace MADE.UnitTests.Common.Models
{
	public class TestModel
	{
		public bool Boolean { get; set; }

		public byte Byte { get; set; }

		public sbyte SByte { get; set; }

		public char Char { get; set; }

		public decimal Decimal { get; set; }

		public double Double { get; set; }

		public float Float { get; set; }

		public int Int { get; set; }

		public uint UInt { get; set; }

		public long Long { get; set; }

		public ulong ULong { get; set; }

		public object Object { get; set; }

		public short Short { get; set; }

		public ushort UShort { get; set; }

		public string String { get; set; }

		public override bool Equals(object obj)
		{
			return obj != null && this.GetType() == obj.GetType() && this.Equals((TestModel)obj);
		}

		protected bool Equals(TestModel other)
		{
			return this.Boolean == other.Boolean && this.Byte == other.Byte && this.SByte == other.SByte
			       && this.Char == other.Char && this.Decimal == other.Decimal && this.Double.Equals(other.Double)
			       && this.Float.Equals(other.Float) && this.Int == other.Int && this.UInt == other.UInt
			       && this.Long == other.Long && this.ULong == other.ULong && Equals(this.Object, other.Object)
			       && this.Short == other.Short && this.UShort == other.UShort && string.Equals(this.String, other.String);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int hashCode = this.Boolean.GetHashCode();
				hashCode = (hashCode * 397) ^ this.Byte.GetHashCode();
				hashCode = (hashCode * 397) ^ this.SByte.GetHashCode();
				hashCode = (hashCode * 397) ^ this.Char.GetHashCode();
				hashCode = (hashCode * 397) ^ this.Decimal.GetHashCode();
				hashCode = (hashCode * 397) ^ this.Double.GetHashCode();
				hashCode = (hashCode * 397) ^ this.Float.GetHashCode();
				hashCode = (hashCode * 397) ^ this.Int;
				hashCode = (hashCode * 397) ^ (int)this.UInt;
				hashCode = (hashCode * 397) ^ this.Long.GetHashCode();
				hashCode = (hashCode * 397) ^ this.ULong.GetHashCode();
				hashCode = (hashCode * 397) ^ (this.Object != null ? this.Object.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ this.Short.GetHashCode();
				hashCode = (hashCode * 397) ^ this.UShort.GetHashCode();
				hashCode = (hashCode * 397) ^ (this.String != null ? this.String.GetHashCode() : 0);
				return hashCode;
			}
		}
	}
}