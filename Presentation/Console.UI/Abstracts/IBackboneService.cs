using ConsoleUI.Models;

namespace ConsoleUI.Abstracts;

public interface IBackboneService
{
	Task Execute(Options option);
}
