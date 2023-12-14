using System;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
	public class PokemonRepository : IPokemonRepository
    {
		private readonly DataContext _context;

		public PokemonRepository(DataContext context)
		{
			_context = context;
		}

        public Pokemon? GetPokemon(int id)
        {
            return _context.Pokemon.FirstOrDefault(p => p.Id == id);
        }

        public Pokemon? GetPokemon(string name)
        {
            return _context.Pokemon.FirstOrDefault(p => p.Name == name);
        }

        public decimal GetPokemonRating(int pokeId)
        {
            var review = _context.Reviews.Where(p => p.Pokemon.Id == pokeId);

            if (!review.Any())
                return 0;

            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public ICollection<Pokemon> GetPokemons()
		{
			return _context.Pokemon.OrderBy(p => p.Id).ToList();
		}

        public bool PokemonExists(int pokeId)
        {
            return _context.Pokemon.Any(p => p.Id == pokeId);
        }
    }
}

