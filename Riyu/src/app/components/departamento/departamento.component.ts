import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CrudService } from '../../services/crud.service';
import { Funcionario } from '../../Models/Funcionarios';
import { Departamento } from '../../Models/Departamentos';
import { FormGroup, FormsModule, ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';


@Component({
  selector: 'app-departamento',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './departamento.component.html',
  styleUrl: './departamento.component.css',
})

export class DepartamentoComponent implements OnInit {
  // Variáveis que serão utilizadas
  departamentos: Departamento[] = [];
  departamentosGeral: Departamento[] = [];
  funcionarios: Funcionario[] = [];

  novoDepartamentoForm!: FormGroup;

  showModal = false;
  isEditMode = false;
  showModalExclusao = false;
  modalButtonText = 'Adicionar';

  departamentoSelecionado: Departamento | null = null;
  departamentoSelecionadoExclusao: Departamento | null = null;

  // Acessar os serviços da API e de formulário
  constructor(private departamentoService: CrudService, private fb: FormBuilder) {}

  // Método chamado ao iniciar o componente
  ngOnInit(): void {
    // Obtém a lista de todos os departamentos e salvar em variáveis
    this.departamentoService.GetDepartamentos().subscribe(data => {
      this.departamentos = this.departamentosGeral = data.dados;
    });

    // Inicializa o formulário vazio para novo departamento
    this.novoDepartamentoForm = this.fb.group({
      nome: ['', Validators.required],
      sigla: ['', Validators.required]
    });
  }

  // Método para filtrar departamentos com base na pesquisa
  search(event: Event) {
    // Obtém o valor da caixa de pesquisa e converte para minúsculas
    const value = (event.target as HTMLInputElement).value.toLowerCase();
    // Converte o valor para número, se possível
    const searchNumber = Number(value);

    // Filtra os departamentos com base no valor da pesquisa
    this.departamentos = this.departamentosGeral.filter(departamento => 
      // Compara o valor da pesquisa com o nome, sigla e ID do departamento
      departamento.nome.toLowerCase().includes(value) ||
      departamento.sigla.toLowerCase().includes(value) ||
      departamento.id === searchNumber
    );
  }

  // Método para abrir o modal para adicionar/editar
  abrirModal(editar = false, id?: number) {
    // Define se o modal está no modo de edição ou adição
    this.isEditMode = editar;
    this.modalButtonText = editar ? 'Editar' : 'Adicionar';

    // Se estiver no modo de edição e houver um ID fornecido
    if (editar && id) {
      // Obtém os detalhes do departamento para edição
      this.departamentoService.GetDepartamento(id).subscribe(response => {
        // Armazena os detalhes do departamento selecionado
        this.departamentoSelecionado = response.dados;

        // Preenche o formulário com os detalhes do departamento selecionado
        this.novoDepartamentoForm.patchValue(this.departamentoSelecionado);
      });
    }

    // Exibe o modal
    this.showModal = true;
  }

  // Método para abrir o modal de exclusão
  abrirModalExclusao(departamento: Departamento) {
    this.departamentoSelecionadoExclusao = departamento;
    this.showModalExclusao = true;
  }

  // Método para fechar o modal de adição/editação 
  fecharModal() {
    this.showModal = false;
    this.novoDepartamentoForm.reset();
  }

  // Método para submeter o formulário de adição/editação
  submit() {
    // Obtém os dados do formulário
    const departamentoData = this.novoDepartamentoForm.value;

    // Determina qual método de serviço deve ser chamado
    const serviceMethod = this.isEditMode ? 'PutDepartamento' : 'PostDepartamento';

    // Se estiver no modo de edição e houver um departamento selecionado
    if (this.isEditMode && this.departamentoSelecionado) {
      // Atribui o ID do departamento selecionado aos dados do formulário
      departamentoData.id = this.departamentoSelecionado.id;
    }

    // Chama o serviço correspondente para adicionar ou editar
    this.departamentoService[serviceMethod](departamentoData).subscribe(
      () => {
        // Fecha o modal e recarrega a página após a conclusão bem-sucedida
        this.fecharModal();
        window.location.reload();
      },
      (error) => {
        console.error(`Erro ao ${this.isEditMode ? 'editar' : 'adicionar'} departamento:`, error);
      }
    );
  }

  // Método para a exclusão de departamento
  submitExclusao() {
    // Pegar o id do departamento
    const idExclusao = this.departamentoSelecionadoExclusao?.id;

    if (idExclusao) {
      // Obtém a lista de funcionários
      this.departamentoService.GetFuncionarios().subscribe(
        response => {
          if (response.sucesso) {
            // Filtrar funcionários apenas do departamento
            const funcionariosExclusao = response.dados.filter(funcionario => funcionario.departamentoId === idExclusao);
            if (funcionariosExclusao.length > 0) {
              // Exclui cada funcionário associado ao departamento
              funcionariosExclusao.forEach(funcionario => {
                if (funcionario.id !== undefined) {
                  this.departamentoService.DeleteFuncionario(funcionario.id).subscribe(
                    () => console.log('Funcionário excluído com sucesso.'),
                    (error) => console.error('Erro ao excluir funcionario:', error)
                  );
                } else {
                  console.error('ID do funcionário excluído é indefinido.');
                }
              });
            }

            // Excluir o departamento após excluir os funcionários associados
            this.departamentoService.DeleteDepartamento(idExclusao).subscribe(
              () => {
                this.fecharModalExclusao();
                window.location.reload();
              },
              (error) => console.error('Erro ao excluir departamento:', error)
            );
          } else {
            console.error('Falha ao obter a lista de funcionários:', response.mensagem);
          }
        },
        error => console.error('Erro ao fazer a requisição:', error)
      );
    }
  }

  // Método para fechar o modal de exclusão
  fecharModalExclusao() {
    this.showModalExclusao = false;
  }
}
