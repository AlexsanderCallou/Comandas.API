using Comandas.Data.Interface;
using Comandas.Services.Interface;
using Comandas.Shared.DTOs;

namespace Comandas.Services.Implementation;

public class MesaService : IMesaService
{
    private readonly IMesaRepository _mesaRepository;
    
    public MesaService(IMesaRepository mesaRepository)
    {
        _mesaRepository = mesaRepository;
    }

    public async Task<MesaGetDTO?> GetMesa(int Id)
    {
        return await _mesaRepository.ReturnMesa(Id);
    }

    public async Task<IEnumerable<MesaGetDTO>> GetMesas()
    {
        return await _mesaRepository.ReturnMesas();
    }

    public async Task<MesaResponsePostDTO> PostMesa(MesaPostDTO mesaPostDTO)
    {
        try
        {
            return await _mesaRepository.CreateMesa(mesaPostDTO);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<bool> PutMesa(MesaPutDTO mesaPutDTO)
    {
        try
        {
            return await _mesaRepository.AtualizaMesa(mesaPutDTO);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public async Task<bool> DeleteMesa(int Id)
    {
        try
        {
            return await _mesaRepository.DeleteMesa(Id);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public Task<bool> MesaDesocupada(int id)
    {
        return _mesaRepository.ReturnMesaDisponivel(id);
    }

    public Task<bool> MesaExiste(int id)
    {
        return _mesaRepository.ReturnMesaExiste(id);
    }
}