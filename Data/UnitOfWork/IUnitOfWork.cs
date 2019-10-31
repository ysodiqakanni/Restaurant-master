
using Data.RepoInterface;
using DomainModel;
using System;

namespace Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRestaurantRepository RestaurantRepository { get; set; }
        IMealTypeRepository MealTypeRepository { get; set; }
        IMealCategoryRepository MealCategoryRepository { get; set; }
        IMealRepository MealRepository { get; set; }
        IRestaurantCategoryRepository RestaurantCategoryRepository { get; set; }
        IAreaRepository AreaRepository { get; set; }

        int Complete();
    }
}