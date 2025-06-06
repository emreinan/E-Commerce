﻿using Core.Persistence.Domain;

namespace Domain.Entities;

public class ProductImage : Entity<Guid>
{
    public Guid ProductId { get; set; }
    public string ImageUrl { get; set; } = default!;
    public bool IsMain { get; set; }

    public virtual Product Product { get; set; } = default!;
}
