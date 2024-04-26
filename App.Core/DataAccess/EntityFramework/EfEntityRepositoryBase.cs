using App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.DataAccess.EntityFramework
{
	public class EfEntityRepositoryBase<TEntity, TContext>
		: IEntityRepository<TEntity>
		where TEntity : class, IEntity, new()
		where TContext : DbContext, new()
	{

		public void Add(TEntity entity)
		{
			using (var context = new TContext())
			{
				var addedEntity = context.Entry(entity);
				addedEntity.State = EntityState.Added;
				context.SaveChanges();
			}
		}

		public void Delete(TEntity entity)
		{
			using (var context = new TContext())
			{
				var deletedEntity = context.Entry(entity);
				deletedEntity.State = EntityState.Deleted;
				context.SaveChanges();
			}
		}

		public TEntity Get(Expression<Func<TEntity, bool>> filter = null)
		{
			using (var context = new TContext())
			{
				return context.Set<TEntity>().SingleOrDefault(filter);
			}
		}

		public virtual List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
		{

			using (var context = new TContext())
			{
				return filter == null
					? context.Set<TEntity>().ToList()
					: context.Set<TEntity>().Where(filter).ToList();
			}
		}

		public void Update(TEntity entity)
		{
			using (var context = new TContext())
			{
				var updateEntity = context.Entry(entity);

				if (updateEntity.State == EntityState.Detached)
				{
					// If the entity is detached, attach it to the context
					context.Set<TEntity>().Attach(entity);
				}

				// Mark the entity as modified
				updateEntity.State = EntityState.Modified;

				try
				{
					// Save changes to the database
					context.SaveChanges();
				}
				catch (DbUpdateConcurrencyException ex)
				{
					// Handle concurrency conflict here if needed
					// For example, you can log the conflict or take appropriate action.
					// This exception is thrown when another user has modified the same record concurrently.
					// You can access the conflicting entity using ex.Entries
					throw;
				}
			}
		}

	}
}
