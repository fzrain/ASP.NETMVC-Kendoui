using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Fzrain.Core.Data;
using Fzrain.Data.Initializers;

namespace Fzrain.Data
{
    public class SqlServerDataProvider : IDataProvider
    {
        #region Utilities

        protected virtual string[] ParseCommands(string filePath, bool throwExceptionIfNonExists)
        {
            if (!File.Exists(filePath))
            {
                if (throwExceptionIfNonExists)
                    throw new ArgumentException(string.Format("Specified file doesn't exist - {0}", filePath));
                return new string[0];
            }


            var statements = new List<string>();
            using (var stream = File.OpenRead(filePath))
            using (var reader = new StreamReader(stream))
            {
                string statement;
                while ((statement = ReadNextStatementFromStream(reader)) != null)
                {
                    statements.Add(statement);
                }
            }

            return statements.ToArray();
        }

        protected virtual string ReadNextStatementFromStream(StreamReader reader)
        {
            var sb = new StringBuilder();

            while (true)
            {
                var lineOfText = reader.ReadLine();
                if (lineOfText == null)
                {
                    return sb.Length > 0 ? sb.ToString() : null;
                }

                if (lineOfText.TrimEnd().ToUpper() == "GO")
                    break;

                sb.Append(lineOfText + Environment.NewLine);
            }

            return sb.ToString();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 初始化dbfactory
        /// </summary>
        public virtual void InitConnectionFactory()
        {
            var connectionFactory = new SqlConnectionFactory();
            //TODO fix compilation warning (below)
#pragma warning disable 0618
            Database.DefaultConnectionFactory = connectionFactory;
        }

        /// <summary>
        /// 初始化数据库
        /// </summary>
        public virtual void InitDatabase()
        {
            InitConnectionFactory();
            SetDatabaseInitializer();
        }

        /// <summary>
        /// 设置初始化策略
        /// </summary>
        public virtual void SetDatabaseInitializer()
        {

            var initializer = new MigrateDatabaseToLatestVersion<FzrainContext, Configuration>();
            //   var initializer = new DropCreateDatabaseIfModelChanges<FzrainContext>();
            Database.SetInitializer(initializer);


        }

        /// <summary>
        /// A value indicating whether this data provider supports stored procedures
        /// </summary>
        public virtual bool StoredProceduredSupported
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a support database parameter object (used by stored procedures)
        /// </summary>
        /// <returns>Parameter</returns>
        public virtual DbParameter GetParameter()
        {
            return new SqlParameter();
        }

        #endregion
    }
}
