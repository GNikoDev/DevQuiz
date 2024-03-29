﻿using System;
using DevQuiz.Shared.Models;
using DevQuiz.Shared.Models.Abstractions;

namespace DevQuiz.Admin.Core.Models.Dto
{
    /// <summary>
    ///     Base dto model
    /// </summary>
    /// <typeparam name="TKey">Type of entry unique identifier </typeparam>
    public class DtoBase<TKey> : IHasKey<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        ///     Get or set unique identifier of entry
        /// </summary>
        public TKey Id { get; set; }
    }
}