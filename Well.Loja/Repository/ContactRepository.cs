using Dapper;
using System.Data;
using Well.Loja.Data;
using Well.Loja.Dto;
using Well.Loja.Models;

namespace Well.Loja.Repository
{
    public class ContactRepository : IContactRepository
    {

        private DbSession _db;

        public ContactRepository(DbSession dbSession)
        {
            this._db = dbSession;
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            using (var conn = _db.Connection)
            {
                string query = @"SELECT * FROM tbl_contact";

                List<Contact> contacts = (await conn.QueryAsync<Contact>(sql : query))
                    .ToList();
                

                return contacts;
            }
        }

        public async Task<Contact> GetByIdAsync(int id)
        {
            using (var conn = _db.Connection)
            {
                string query = @"SELECT * FROM tbl_contact where id = @id";

                Contact contact = await conn
                    .QueryFirstOrDefaultAsync<Contact>(sql: query, param: new { id });

                return contact;
            }
        }

        public async Task<int> PostAsync(ContactForCreationDto newContact)
        {
            string query = @"INSERT INTO tbl_contact(name, email, phone) VALUES(@name, @email, @phone)";
            
            var parameters = new DynamicParameters();
            parameters.Add("name", newContact.Name, DbType.String);
            parameters.Add("email", newContact.Email, DbType.String);
            parameters.Add("phone", newContact.Phone, DbType.String);

            using (var conn = _db.Connection)
            {
                
                var result = await conn.ExecuteAsync(sql: query, param: parameters);

                

                return result;
            }
        }

        public async Task<int> UpdateAsync(int id, ContactForUpdateDto newContact)
        {
            using (var conn = _db.Connection)
            {
                string query=@"UPDATE tbl_contact SET Name=@name, Email=@email, Phone=@phone WHERE Id=@id";

                var parameters = new DynamicParameters();
                parameters.Add("id", id);
                parameters.Add("name", newContact.Name);
                parameters.Add("email", newContact.Email);
                parameters.Add("phone", newContact.Phone);

                
                var result = await conn
                    .ExecuteAsync(sql: query,param: parameters);

                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var conn = _db.Connection)
            {
                string query = @"DELETE FROM tbl_contact WHERE id = @id";

                var result = await conn.ExecuteAsync(sql: query, param: new { id });

                return result;
            }
        }
    }
}
