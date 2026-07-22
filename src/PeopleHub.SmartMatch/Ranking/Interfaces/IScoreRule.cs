using PeopleHub.SmartMatch.Models;

namespace PeopleHub.SmartMatch.Ranking.Interfaces;

public interface IScoreRule
{
    decimal Calculate(ScoreContext context);
}