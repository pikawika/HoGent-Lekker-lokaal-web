using System;
using System.Collections.Generic;
using System.Linq;
using LekkerLokaal.Models.Domain;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace LekkerLokaal.Data.Repositories
{
    public class BonRepository : IBonRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Bon> _bonnen;
        public BonRepository(ApplicationDbContext context)
        {
            _context = context;
            _bonnen = context.Bonnen;
        }
        public IEnumerable<Bon> GetAll()
        {
            return _bonnen.Include(b => b.Categorie).OrderByDescending(b => b.AantalBesteld).AsNoTracking().ToList();
        }

        public IEnumerable<Bon> GetTop3(IEnumerable<Bon> inputlijst)
        {
            return inputlijst.OrderByDescending(b => b.AantalBesteld).Take(3).ToList();
        }

        public IEnumerable<Bon> GetAlles(string zoekKey, IEnumerable<Bon> inputlijst)
        {
            if (zoekKey.Trim().Length != 0)
            {
                string[] _woorden = zoekKey.ToLower().Split(' ');
                int _aantalWoorden = _woorden.Length;
                List<Bon> _advancedSearch = new List<Bon>();


                foreach (Bon b in inputlijst)
                {
                    int _aantalMatchenWoord = 0;
                    foreach (String woord in _woorden)
                    {
                        String woordfilter = woord.Replace("-", "");
                        woordfilter = woordfilter.Replace("_", "");
                        woordfilter = VerwijderAccenten(woordfilter);

                        bool matchFound = false;
                        foreach (String woordToMatch in b.Categorie.Naam.ToLower().Split(' '))
                        {
                            String woordfilterMatch = woordToMatch.Replace("-", "");
                            woordfilterMatch = woordfilterMatch.Replace("_", "");
                            woordfilterMatch = VerwijderAccenten(woordfilterMatch);
                            if (woordfilterMatch.Contains(woordfilter))
                            {
                                matchFound = true;
                                _aantalMatchenWoord++;
                            }
                        }
                        if (matchFound == false)
                        {
                            foreach (String woordToMatch in b.Gemeente.ToLower().Split(' '))
                            {
                                String woordfilterMatch = woordToMatch.Replace("-", "");
                                woordfilterMatch = woordfilterMatch.Replace("_", "");
                                woordfilterMatch = VerwijderAccenten(woordfilterMatch);
                                if (woordfilterMatch.Contains(woordfilter))
                                {
                                    matchFound = true;
                                    _aantalMatchenWoord++;
                                }
                            }
                        }
                        if (matchFound == false)
                        {
                            foreach (String woordToMatch in b.Naam.ToLower().Split(' '))
                            {
                                String woordfilterMatch = woordToMatch.Replace("-", "");
                                woordfilterMatch = woordfilterMatch.Replace("_", "");
                                woordfilterMatch = VerwijderAccenten(woordfilterMatch);
                                if (woordfilterMatch.Contains(woordfilter))
                                {
                                    matchFound = true;
                                    _aantalMatchenWoord++;
                                }
                            }
                        }
                        
                        
                    }
                    if (_aantalMatchenWoord == _aantalWoorden)
                    {
                        _advancedSearch.Add(b);
                    }
                }

                return _advancedSearch.ToList();
            }
            else
            {
                return GetAll().ToList();
            }
            
        }

        public IEnumerable<Bon> GetByCategorie(string zoekKey, IEnumerable<Bon> inputlijst)
        {
            string _zoekKey = zoekKey.ToLower();
            return inputlijst.Where(b => b.Categorie.Naam.ToLower().Contains(_zoekKey)).ToList();
        }

        public IEnumerable<Bon> GetByLigging(string zoekKey, IEnumerable<Bon> inputlijst)
        {
            string _zoekKey = zoekKey.ToLower();
            return inputlijst.Where(b => b.Gemeente.ToLower().Contains(_zoekKey)).ToList();
        }

        public IEnumerable<Bon> GetByNaam(string zoekKey, IEnumerable<Bon> inputlijst)
        {
            string _zoekKey = zoekKey.ToLower();
            return inputlijst.Where(b => b.Naam.ToLower().Contains(_zoekKey)).ToList();
        }

        public IEnumerable<Bon> GetByPrijs(int zoekKey, IEnumerable<Bon> inputlijst)
        {
            return inputlijst.Where(b => b.MinPrijs <= zoekKey).ToList();
        }

        public string VerwijderAccenten(string input)
        {
            string stFormD = input.Normalize(NormalizationForm.FormD);
            int len = stFormD.Length;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stFormD[i]);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[i]);
                }
            }
            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }
    }
}
