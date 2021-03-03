using GuessWho.Execution.Dtos;
using System.Threading.Tasks;

namespace GuessWho.Execution.Contracts
{
    public interface IIdolCrud
    {
        Task<IdolDto> CreateIdol(CreateIdolDto createIdolDto);

        Task<IdolDto> UpdateIdol(UpdateIdolDto updateIdolDto);

        Task DeleteIdol(DeleteIdolDto deleteIdolDto);
    }
}
