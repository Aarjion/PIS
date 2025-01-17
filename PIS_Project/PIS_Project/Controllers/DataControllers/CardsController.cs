﻿using PIS_Project.Models.DataClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PIS_Project.Controllers.DataControllers
{
    public class CardsController : DbContext
    {
        internal DbSet<Card> Card { get; set; }
        internal DbSet<Status> Status { get; private set; }
        internal DbSet<MUS> MUS { get; private set; }
        internal Status GetStatusByID(int id)
        {
            return Status.FirstOrDefault(i => i.ID == id);
        }
        internal MUS GetMUByID(int id)
        {
            return MUS.FirstOrDefault(i => i.ID == id);
        }
        public virtual List<Card> GetCards()
        {
            var result = Card.ToList();
            foreach (var card in result)
            {
                card.Status = GetStatusByID(card.id_status).Name;
                card.MU = GetMUByID(card.ID_MU).Name;
            }
            return result;
        }

        public List<Card> GetFilteredBy(Dictionary<string, object> filters)
        {
            var result = GetCards();
            foreach (var filter in filters)
            {
                var prop = (new Card()).GetType().GetProperty(filter.Key);
                result = result.Where(i => prop.GetValue(i) == filter.Value).ToList();
            }

                foreach (var card in result)
                {
                    card.Status = GetStatusByID(card.id_status).Name;
                    card.MU = GetMUByID(card.ID_MU).Name;
                }
            
            return result;
        }
        public List<Card> GetSortedBy(Dictionary<string, bool> filters)
        {
            var result = Card.ToList();
            foreach (var filter in filters)
            {
                var prop = (new Card()).GetType().GetProperty(filter.Key);
                if (filter.Value)
                    result = result.OrderBy(i => prop.GetValue(i)).ToList();
                else
                    result = result.OrderBy(i => prop.GetValue(i)).Reverse().ToList();
            }

                foreach (var card in result)
                {
                    card.Status = GetStatusByID(card.id_status).Name;
                    card.MU = GetMUByID(card.ID_MU).Name;
                }
            
            return result;
        }
        public CardsController()
            : base("DBConnection") 
        {
           
            Card = Set<Card>();
            Status = Set<Status>();
            MUS = Set<MUS>();
        }
        public virtual Card GetCardByID(int id)
        {
            var card = Card.FirstOrDefault(i => i.ID == id);
            card.Status = GetStatusByID(card.id_status).Name;
            card.MU = GetMUByID(card.ID_MU).Name;
            return card;
        }
        
    }
}