using Microsoft.EntityFrameworkCore.Storage;
using Infrastructure.Repositories;
using Infrastructure.Data;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.UnitOfWork;

namespace Infrastructure.UOWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;
        private readonly Dictionary<Type, object> _repositories;
        private static int _instanceCount = 0;
        public static int InstanceCount => _instanceCount;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _repositories = [];
            _instanceCount++;
            Console.WriteLine($"Current UnitOfWork instance count: {InstanceCount}");
        }

        // Trả về repository động và đảm bảo chỉ có một instance
        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);

            // Kiểm tra xem repository đã tồn tại chưa
            if (_repositories.ContainsKey(type))
            {
                return (IGenericRepository<T>)_repositories[type];
            }

            // Nếu chưa tồn tại, tạo mới và lưu vào Dictionary
            var repository = new GenericRepository<T>(_context);
            _repositories.Add(type, repository);

            return (IGenericRepository<T>)repository;
        }

        // Lưu thay đổi đồng bộ
        public void Save()
        {
            _context.SaveChanges();
        }

        // Lưu thay đổi bất đồng bộ
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        // Bắt đầu giao dịch
        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        // Commit giao dịch
        public void CommitTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction.Dispose();
                _transaction = null;
            }
        }

        // Rollback giao dịch
        public void RollBack()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
                _transaction = null;
            }
        }

        // Dispose để giải phóng tài nguyên
        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
            _context.Dispose();
        }
    }
}
