using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Data.SqlClient;
using System.Data;

public class SqlSupportTransaction
{
    public string Query { get; set; }

    public CommandType CommandType { get; set; }

    public List<SqlParameter> Parameters { get; set; } = new List<SqlParameter>();
}
