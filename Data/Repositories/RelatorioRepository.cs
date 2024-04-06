using Atendimento_API.Interfaces;
using Atendimento_API.Models;
using Dapper;
using System.Data;

namespace Atendimento_API.Data.Repositories
{
    public class RelatorioRepository : IRelatorioRepository
    {
        private readonly DatabaseContext _context;

        public RelatorioRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Relatorio>> GetAllAsync()
        {
            using IDbConnection dbConnection = _context.Connection;
            dbConnection.Open();
            return await dbConnection.QueryAsync<Relatorio>("SELECT TOP (1000) relatorio_id AS id, data_relatorio AS dataRelatorio, quant_senhas_emitidas AS qtdSenhasEmitidas, quant_senhas_atendidas AS qtdSenhasAtendidas, quant_senhas_emitidas_SP AS qtdSenhasEmitidasSP, quant_senhas_atendidas_SP AS qtdSenhasAtendidasSP, quant_senhas_emitidas_SG AS qtdSenhasEmitidasSG, quant_senhas_atendidas_SG AS qtdSenhasAtendidasSG, quant_senhas_emitidas_SE AS qtdSenhasEmitidasSE, quant_senhas_atendidas_SE AS qtdSenhasAtendidasSE, tempo_medio_atendimento_SP AS tempoMedioAtendimentoSP, tempo_medio_atendimento_SG AS tempoMedioAtendimentoSG, tempo_medio_atendimento_SE AS tempoMedioAtendimentoSE FROM atendimento.dbo.Relatorios");
        }

        public async Task<Relatorio> GetByIdAsync(int id)
        {
            using IDbConnection dbConnection = _context.Connection;
            dbConnection.Open();
            return await dbConnection.QueryFirstOrDefaultAsync<Relatorio>("SELECT relatorio_id AS id, data_relatorio AS dataRelatorio, quant_senhas_emitidas AS qtdSenhasEmitidas, quant_senhas_atendidas AS qtdSenhasAtendidas, quant_senhas_emitidas_SP AS qtdSenhasEmitidasSP, quant_senhas_atendidas_SP AS qtdSenhasAtendidasSP, quant_senhas_emitidas_SG AS qtdSenhasEmitidasSG, quant_senhas_atendidas_SG AS qtdSenhasAtendidasSG, quant_senhas_emitidas_SE AS qtdSenhasEmitidasSE, quant_senhas_atendidas_SE AS qtdSenhasAtendidasSE, tempo_medio_atendimento_SP AS tempoMedioAtendimentoSP, tempo_medio_atendimento_SG AS tempoMedioAtendimentoSG, tempo_medio_atendimento_SE AS tempoMedioAtendimentoSE FROM atendimento.dbo.Relatorios WHERE relatorio_id = @Id", new { Id = id });
        }

        public async Task AddAsync(Relatorio relatorio)
        {
            using IDbConnection dbConnection = _context.Connection;
            dbConnection.Open();
            await dbConnection.ExecuteAsync(@"INSERT INTO atendimento.dbo.Relatorios 
                                    (data_relatorio, quant_senhas_emitidas, quant_senhas_atendidas, 
                                     quant_senhas_emitidas_SP, quant_senhas_atendidas_SP, 
                                     quant_senhas_emitidas_SG, quant_senhas_atendidas_SG, 
                                     quant_senhas_emitidas_SE, quant_senhas_atendidas_SE, 
                                     tempo_medio_atendimento_SP, tempo_medio_atendimento_SG, 
                                     tempo_medio_atendimento_SE) 
                                 VALUES 
                                    (@DataRelatorio, @QtdSenhasEmitidas, @QtdSenhasAtendidas, 
                                     @QtdSenhasEmitidasSP, @QtdSenhasAtendidasSP, 
                                     @QtdSenhasEmitidasSG, @QtdSenhasAtendidasSG, 
                                     @QtdSenhasEmitidasSE, @QtdSenhasAtendidasSE, 
                                     @TempoMedioAtendimentoSP, @TempoMedioAtendimentoSG, 
                                     @TempoMedioAtendimentoSE)",
                                             new
                                             {
                                                 DataRelatorio = relatorio.dataRelatorio,
                                                 QtdSenhasEmitidas = relatorio.qtdSenhasEmitidas,
                                                 QtdSenhasAtendidas = relatorio.qtdSenhasAtendidas,
                                                 QtdSenhasEmitidasSP = relatorio.qtdSenhasEmitidasSP,
                                                 QtdSenhasAtendidasSP = relatorio.qtdSenhasAtendidasSP,
                                                 QtdSenhasEmitidasSG = relatorio.qtdSenhasEmitidasSG,
                                                 QtdSenhasAtendidasSG = relatorio.qtdSenhasAtendidasSG,
                                                 QtdSenhasEmitidasSE = relatorio.qtdSenhasEmitidasSE,
                                                 QtdSenhasAtendidasSE = relatorio.qtdSenhasAtendidasSE,
                                                 TempoMedioAtendimentoSP = relatorio.tempoMedioAtendimentoSP,
                                                 TempoMedioAtendimentoSG = relatorio.tempoMedioAtendimentoSG,
                                                 TempoMedioAtendimentoSE = relatorio.tempoMedioAtendimentoSE
                                             });
        }

        public async Task UpdateAsync(Relatorio relatorio)
        {
            using IDbConnection dbConnection = _context.Connection;
            dbConnection.Open();
            await dbConnection.ExecuteAsync(@"UPDATE atendimento.dbo.Relatorios 
                                    SET 
                                        data_relatorio = @DataRelatorio, 
                                        quant_senhas_emitidas = @QtdSenhasEmitidas, 
                                        quant_senhas_atendidas = @QtdSenhasAtendidas, 
                                        quant_senhas_emitidas_SP = @QtdSenhasEmitidasSP, 
                                        quant_senhas_atendidas_SP = @QtdSenhasAtendidasSP, 
                                        quant_senhas_emitidas_SG = @QtdSenhasEmitidasSG, 
                                        quant_senhas_atendidas_SG = @QtdSenhasAtendidasSG, 
                                        quant_senhas_emitidas_SE = @QtdSenhasEmitidasSE, 
                                        quant_senhas_atendidas_SE = @QtdSenhasAtendidasSE, 
                                        tempo_medio_atendimento_SP = @TempoMedioAtendimentoSP, 
                                        tempo_medio_atendimento_SG = @TempoMedioAtendimentoSG, 
                                        tempo_medio_atendimento_SE = @TempoMedioAtendimentoSE 
                                    WHERE 
                                        relatorio_id = @RelatorioId",
                                             new
                                             {
                                                 DataRelatorio = relatorio.dataRelatorio,
                                                 QtdSenhasEmitidas = relatorio.qtdSenhasEmitidas,
                                                 QtdSenhasAtendidas = relatorio.qtdSenhasAtendidas,
                                                 QtdSenhasEmitidasSP = relatorio.qtdSenhasEmitidasSP,
                                                 QtdSenhasAtendidasSP = relatorio.qtdSenhasAtendidasSP,
                                                 QtdSenhasEmitidasSG = relatorio.qtdSenhasEmitidasSG,
                                                 QtdSenhasAtendidasSG = relatorio.qtdSenhasAtendidasSG,
                                                 QtdSenhasEmitidasSE = relatorio.qtdSenhasEmitidasSE,
                                                 QtdSenhasAtendidasSE = relatorio.qtdSenhasAtendidasSE,
                                                 TempoMedioAtendimentoSP = relatorio.tempoMedioAtendimentoSP,
                                                 TempoMedioAtendimentoSG = relatorio.tempoMedioAtendimentoSG,
                                                 TempoMedioAtendimentoSE = relatorio.tempoMedioAtendimentoSE,
                                                 RelatorioId = relatorio.id
                                             });
        }

        public async Task DeleteAsync(int id)
        {
            using IDbConnection dbConnection = _context.Connection;
            dbConnection.Open();
            await dbConnection.ExecuteAsync("DELETE FROM atendimento.dbo.Relatorios WHERE relatorio_id = @Id", new { Id = id });
        }
    }
}
