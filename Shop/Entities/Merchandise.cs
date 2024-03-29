﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop
{
    public class Merchandise : ICopyable<Merchandise>, IComparable<Merchandise>
    {
        public Merchandise(Product product, List<MerchandiseCategory> categories, int price, int quantity)
        {
            Product = product;
            Categories = categories;
            Price = price;
            Quantity = quantity;
        }

        public Product Product { get; }
        public int Price { get; private set; }
        public int Quantity { get; private set; }
        public List<MerchandiseCategory> Categories { get; }

        public string Info =>
            $"Продукт - {Product}, цена - {Price}, категории - [{string.Join(", ", Categories)}], количество - {Quantity}";

        public void DecreaseQuantity(int quantity)
        {
            if (quantity >= Quantity)
            {
                throw new ArgumentException(
                    "Количество забираемого продукта должно быть меньше, чем у товара есть сейчас");
            }

            Quantity -= quantity;
        }

        public void IncreaseQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Количество добавляемого продукта в товаре должно быть больше 0");
            }

            Quantity += quantity;
        }

        public Merchandise DeepCopy()
        {
            return new Merchandise(Product.DeepCopy(), Categories.ShallowCopy(), Price, Quantity);
        }

        public Merchandise DeepCopy(int quantity)
        {
            return new Merchandise(Product.DeepCopy(), Categories.ShallowCopy(), Price, quantity);
        }

        public int CompareTo(Merchandise other)
        {
            if (other == null)
                return 1;

            int categoryCountComparison = Categories.Count.CompareTo(other.Categories.Count);

            if (categoryCountComparison != 0)
                return categoryCountComparison;

            string categories = String.Join(",",
                Categories.OrderBy(category => category).Select(category => category.ToString()));

            string otherCategories = String.Join(",",
                other.Categories.OrderBy(category => category).Select(category => category.ToString()));

            int categoriesComparison =
                string.Compare(categories, otherCategories, StringComparison.Ordinal);

            if (categoriesComparison != 0)
                return categoriesComparison;

            int nameComparison = string.Compare(this.Product.Name, other.Product.Name, StringComparison.Ordinal);

            if (nameComparison != 0)
                return nameComparison;

            if (Quantity != other.Quantity)
                return other.Quantity.CompareTo(Quantity);

            int expirationComparison = Product.ExpirationDate.CompareTo(other.Product.ExpirationDate);

            if (expirationComparison != 0)
                return expirationComparison;

            return Price.CompareTo(other.Price);
        }
    }

    public enum MerchandiseCategory
    {
        Food,
        Electronics,
        Clothing
    }
}