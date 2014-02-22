namespace Serenity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    ///   An object that is used to create criterias by employing operator overloading 
    ///   features of C# language, instead of using string based criterias.</summary>
    [Serializable]
    public class Criteria : BaseCriteria
    {
        public static readonly BaseCriteria Empty = new Criteria();
        private string expression;

        /// <summary>
        ///   Creates an empty criteria</summary>
        private Criteria()
        {
        }

        /// <summary>
        ///   Creates a new criteria with given condition. This condition is usually a 
        ///   field name, but it can also be a criteria text pre-generated.</summary>
        /// <remarks>
        ///   Usually used like: <c>new Criteria("fieldname") >= 5</c>.</remarks>
        /// <param name="criteria">
        ///   A field name or criteria condition (can be null)</param>
        public Criteria(string text)
        {
            this.expression = text;
        }

        /// <summary>
        ///   Creates a new criteria that contains field name of the metafield.</summary>
        /// <param name="field">
        ///   Field (required).</param>
        public Criteria(IField field)
        {
            if (field == null)
                throw new ArgumentNullException("field");

            this.expression = field.Expression;
        }

        /// <summary>
        ///   Belirtilen tablo alias'� ve alan ad�n� aralar�na nokta koyarak i�eren yeni bir 
        ///   kriter olu�turur.</summary>
        /// <param name="joinAlias">
        ///   Tablo alias'�. Null ya da bo� olursa �nemsenmez.</param>
        /// <param name="field">
        ///   Alan ad� (zorunlu).</param>
        public Criteria(string alias, string field)
        {
            if (field == null || field.Length == 0)
                throw new ArgumentNullException("field");

            if (alias == null || alias.Length == 0)
                throw new ArgumentNullException("alias");
            this.expression = alias + "." + field;
        }

        /// <summary>
        ///   Belirtilen numerik tablo alias'� (ba��na T konarak) ve alan ad�n� aralar�na 
        ///   nokta koyarak i�eren yeni bir kriter olu�turur.</summary>
        /// <param name="joinNumber">
        ///   Join numaras� (T1 gibi kullan�l�r). De�er s�f�rdan k���kse alan ad� tek ba��na
        ///   kullan�l�r.</param>
        /// <param name="field">
        ///   Alan ad� (zorunlu).</param>
        public Criteria(int joinNumber, string field)
        {
            if (field == null || field.Length == 0)
                throw new ArgumentNullException("field");

            if (joinNumber < 0)
                throw new ArgumentOutOfRangeException("joinNumber");

            this.expression = joinNumber.TableAliasDot() + field;
        }

        /// <summary>
        ///   Belirtilen numerik tablo alias'� (ba��na T konarak) ve alan�n ad�n� aralar�na 
        ///   nokta koyarak i�eren yeni bir kriter olu�turur.</summary>
        /// <param name="joinNumber">
        ///   Join numaras� (T1 gibi kullan�l�r)</param>
        /// <param name="field">
        ///   Alan nesnesi (zorunlu).</param>
        public Criteria(Alias alias, IField field)
            : this(alias.Name, field.Name)
        {
        }

        /// <summary>
        ///   Belirtilen numerik tablo alias'� (ba��na T konarak) ve alan�n ad�n� aralar�na 
        ///   nokta koyarak i�eren yeni bir kriter olu�turur.</summary>
        /// <param name="joinNumber">
        ///   Join numaras� (T1 gibi kullan�l�r)</param>
        /// <param name="field">
        ///   Alan nesnesi (zorunlu).</param>
        public Criteria(Alias alias, string field)
            : this(alias.Name, field)
        {
        }

        /// <summary>
        ///   Belirtilen numerik tablo alias'� (ba��na T konarak) ve alan�n ad�n� aralar�na 
        ///   nokta koyarak i�eren yeni bir kriter olu�turur.</summary>
        /// <param name="joinNumber">
        ///   Join numaras� (T1 gibi kullan�l�r)</param>
        /// <param name="field">
        ///   Alan nesnesi (zorunlu).</param>
        public Criteria(int joinNumber, IField field)
            : this(joinNumber, field.Name)
        {
        }

        /// <summary>
        ///   Belirtilen join ve meta alan�n ad�n� aralar�na nokta koyarak i�eren yeni bir 
        ///   kriter olu�turur.</summary>
        /// <param name="join">
        ///   Tablo alias bilgisini i�eren LeftJoin nesnesi (zorunlu).</param>
        /// <param name="field">
        ///   Field alan (zorunlu).</param>
        public Criteria(string join, IField field)
            : this(join, field.Name)
        {
        }

        /// <summary>
        ///   Belirtilen SqlQuery i i�eren yeni bir 
        ///   kriter olu�turur.</summary>
        /// <param name="query">
        ///   Query nesnesi (genellikle sub query).</param>
        public Criteria(ISqlQuery query)
            : this(query.ToString())
        {
        }


        /// <summary>
        ///   Verilen alan ad�n� k��eli parantez i�ine alarak yeni bir kriter olu�turur.
        ///   SQL'de bo�luk i�eren ya da keyword olan alan adlar�n�n kullan�labilmesi 
        ///   i�in gerekebilir.</summary>
        /// <param name="fieldName">
        ///   K��eli parantez i�ine al�n�p kriterye �evrilecek alan ad� (zorunlu).</param>
        /// <returns>
        ///   Alan ad�n� k��eli parantez i�inde i�eren yeni bir kriter.</returns>
        public static Criteria Bracket(string fieldName)
        {
            if (fieldName == null || fieldName.Length == 0)
                throw new ArgumentNullException("fieldName");

            return new Criteria("[" + fieldName + "]");
        }

        /// <summary>
        ///   Creates a new EXISTS criteria</summary>
        /// <param name="expression">
        ///   Expression</param>
        /// <returns></returns>
        public static BaseCriteria Exists(string expression)
        {
            return new UnaryCriteria(CriteriaOperator.Exists, new Criteria(expression));
        }

        /// <summary>
        ///   Gets if criteria is empty.</summary>
        public override bool IsEmpty
        {
            get 
            {
                return expression.IsEmptyOrNull();
            }
        }

        public override void ToString(StringBuilder sb, IQueryWithParams query)
        {
            sb.Append(this.expression);
        }
    }
}