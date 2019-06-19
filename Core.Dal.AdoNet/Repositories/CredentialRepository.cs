namespace Core.Dal.AdoNet.Repositories
{
    using Core.Dal.AdoNet.Exceptions;
    using Store.Logic.Entity;
    using System;
    using System.Data;

    internal class CredentialRepository : BaseRepository<Credential>
    {
        const string IdField = "Id";
        const string NameCredentialFiled = "NameCredential";
        const string FullNameCredentialFiled = "FullNameCredential";
        const string ParentCredentialidFiled = "ParentCredentialid";
        const string OrderFiled = "Order";
        const string UrlFiled = "Url";

        public CredentialRepository(IDbConnection connection)
            : base(connection, "dbo.Credentials")
        {
            BindViewMapper(IdField, (o, e) => e.Id = Convert.ToInt32(o));
            BindViewMapper(NameCredentialFiled, (o, e) => e.NameCredential = o.ToString());
            BindViewMapper(FullNameCredentialFiled, (o, e) => e.FullNameCredential = o.ToString());
            BindViewMapper(ParentCredentialidFiled, (o, e) => e.ParentCredentialId = Convert.ToInt32(o));
            BindViewMapper(OrderFiled, (o, e) => e.Order = Convert.ToInt32(o));
            BindViewMapper(UrlFiled, (o, e) => e.Url = o.ToString());
        }



        public override bool Add(Credential entity)
        {
            var command = Connection.CreateCommand();
            command.CommandText = $"insert into {BaseTable} " +
                $"({NameCredentialFiled}, {FullNameCredentialFiled}, {ParentCredentialidFiled},{OrderFiled},{UrlFiled}) values " +
                $"( '{entity.NameCredential}', '{entity.FullNameCredential}', '{entity.ParentCredentialId}','{entity.Order}','{entity.Url}') ";
            try
            {
                return command.ExecuteNonQuery() == 1;
            }
            catch (Exception e)
            {
                throw new DalExecutionException("", e);
            }
        }

        public override bool Update(Credential entity)
        {

            var command = Connection.CreateCommand();
            command.CommandText = $"update {BaseTable} " +
                $"set " +
                $"{NameCredentialFiled}='{entity.NameCredential}', " +
                $"{FullNameCredentialFiled}='{entity.FullNameCredential}'," +
                $"{ParentCredentialidFiled}='{entity.ParentCredentialId}'," +
                $"{OrderFiled}='{entity.Order}'," +
                $"{UrlFiled}={entity.Url}  where Id={entity.Id}";
            try
            {
                return command.ExecuteNonQuery() == 1;
            }
            catch (Exception e)
            {
                throw new DalExecutionException("", e);
            }
        }
    }
}
