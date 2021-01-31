using GuessWho.Execution.Dtos;
using System.Threading.Tasks;

namespace GuessWho.Execution.Contracts
{
    public interface IIdolCrud
    {
        Task<IdolDto> CreateConfiguration(CreateIdolDto createIdolDto);

        Task<IdolDto> UpdateConfiguration(UpdateIdolDto updateIdolDto);

        Task DeleteConfiguration(DeleteIdolDto deleteIdolDto);
    }
}
