﻿using System.Collections.Generic;
using System.Linq;

namespace PrismaDB.QueryAST.DDL
{
    public enum AlterType
    {
        ADD,
        DROP,
        MODIFY
    }

    public class AlterTableQuery : DDLQuery
    {
        public TableRef TableName;
        public AlterType AlterType;
        public List<AlteredColumn> AlteredColumns;

        public AlterTableQuery(AlterType type = AlterType.ADD)
            : this(type, new TableRef(""))
        { }

        public AlterTableQuery(AlterType type, string tableName)
            : this(type, new TableRef(tableName))
        { }

        public AlterTableQuery(AlterType type, TableRef table)
        {
            AlterType = type;
            TableName = table.Clone();
            AlteredColumns = new List<AlteredColumn>();
        }

        public AlterTableQuery(AlterTableQuery other)
        {
            TableName = other.TableName.Clone();
            AlterType = other.AlterType;
            AlteredColumns = new List<AlteredColumn>(other.AlteredColumns.Count);
            AlteredColumns.AddRange(other.AlteredColumns.Select(x => x.Clone() as AlteredColumn));
        }

        public override string ToString()
        {
            return DialectResolver.Dialect.AlterTableQueryToString(this);
        }

        public override object Clone()
        {
            return new AlterTableQuery(this);
        }
    }
}
