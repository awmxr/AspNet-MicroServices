using Dapper;
using Discount.Api.Entities;
using Npgsql;

namespace Discount.Api.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {

        #region Constructor
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion


        #region Get Coupon
        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DataBaseSettings:ConnectionString"));

            var coupon = await connection.QueryFirstAsync<Coupon>
                ("SELECT * FROM Coupon WHERE ProductName = @ProductName", new {ProductName = productName});

            if (coupon == null)
            {
                return new Coupon() { ProductName = "No Discount", Description = "No Discount Description", Amount = 0 };
            }
            return coupon;

        }
        #endregion


        #region Create Coupon
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DataBaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync("INSERT INTO Coupon(ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
                new {ProductName = coupon.ProductName,Description = coupon.Description , Amount = coupon.Amount});

            if (affected == 0) return false;

            return true;
        }
        #endregion

        #region Update Coupon
        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DataBaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync("UPDATE Coupon SET ProductName=@ProductName, Description=@Description, Amount=@Amount",
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

            if (affected == 0) return false;

            return true;
        }

        #endregion


        #region Delete Coupon

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DataBaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName",
                new { ProductName = productName });

            if (affected == 0) return false;

            return true;
        }

        #endregion




    }
}
