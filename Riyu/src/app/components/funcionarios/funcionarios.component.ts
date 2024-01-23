import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CrudService } from '../../services/crud.service';
import { Funcionario } from '../../Models/Funcionarios';
import { FormGroup, FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { FormBuilder, Validators } from '@angular/forms';
import { Departamento } from '../../Models/Departamentos';
import { ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-funcionarios',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './funcionarios.component.html',
  styleUrl: './funcionarios.component.css'
})

export class FuncionarioComponent implements OnInit {

  // Construtor do componente, injetando serviços e dependências necessárias
  constructor(private funcionarioService: CrudService, private fb: FormBuilder, private route: ActivatedRoute) {}

  // Propriedades e variáveis de controle do componente
  @ViewChild('fotoInput') fotoInput!: ElementRef;
  fotoPreviewUrl: string | ArrayBuffer | null = null;
  nomeArquivo: string | null = "";
  hasFile: boolean = false;

  // Selecionar um arquivo para upload
  onFileSelected(event: any) {
    const arquivoSelecionado: File = <File>event.target.files[0];

    if (arquivoSelecionado) {
      const arquivoRenomeado = this.renomearArquivo(arquivoSelecionado);
      this.uploadArquivo(arquivoRenomeado);
      this.nomeArquivo = arquivoRenomeado.name;
      this.fotoPreviewUrl = URL.createObjectURL(arquivoRenomeado);
    }
  }

  // Renomear o arquivo antes de upload
  private renomearArquivo(arquivoOriginal: File): File {
    // Obter a data atual em milissegundos
    const dataAtual = new Date().getTime();
    const nomeOriginal = arquivoOriginal.name;
    
    // Substitui caracteres especiais por underscores
    const nomeSemEspeciais = nomeOriginal.replace(/[^\w.-]/gi, '_');
    
    // Novo nome do arquivo
    const nomeNovo = `${dataAtual}_${nomeSemEspeciais}`;
    const arquivoRenomeado = new File([arquivoOriginal], nomeNovo, { type: arquivoOriginal.type });
    return arquivoRenomeado;
  }

  // Efetuar o upload do arquivo
  private uploadArquivo(arquivoRenomeado: File) {
    this.funcionarioService.PostImagem(arquivoRenomeado).subscribe(() => {
      console.log("Arquivo enviado");
    });
  }

  // Arrays para armazenar funcionários e departamentos
  funcionarios: Funcionario[] = [];
  funcionariosGeral: Funcionario[] = [];
  departamentos: Departamento[] = [];

  // Formulário para novo funcionário
  novoFuncionarioForm!: FormGroup;
  departamentoDesejadoId: number | undefined;

  // Inicializar formulário vazio
  ngOnInit(): void {
    // Obter a lista de departamentos
    this.funcionarioService.GetDepartamentos().subscribe(departamentos => {
      this.departamentos = departamentos.dados;

      // Obter o nome do departamento a partir da URL
      const nomeDepartamentoNaURL = this.route.snapshot.paramMap.get('nome');

      // Encontrar o departamento correspondente com base no nome da URL
      const departamentoDesejado = this.departamentos.find(departamento => departamento.nome.split(' ').join('') === nomeDepartamentoNaURL);
  
      // Verificar se o departamento foi encontrado
      if (departamentoDesejado) {
        this.departamentoDesejadoId = departamentoDesejado.id;
        // Verificar quais funcionários pertencem a esse departamento
        this.funcionarioService.GetFuncionarios().subscribe(data => {
          this.funcionarios = data.dados.filter(funcionario => funcionario.departamentoId === departamentoDesejado.id);
          this.funcionariosGeral = this.funcionarios;
        });
      } else {
        console.error('Departamento não encontrado na URL');
        // Lidar com a situação em que o departamento não é encontrado na URL
      }
    });
  
    // Inicialize o novoFuncionarioForm usando FormBuilder com validadores
    this.nomeArquivo = "";
    this.novoFuncionarioForm = this.fb.group({
      nome: ['', Validators.required],
      rg: ['', Validators.required],
      foto: ['', null],
    });
  }

  // Filtrar funcionários com base na busca
  search(event: Event) {
    const target = event.target as HTMLInputElement;
    const value = target.value.toLowerCase();
    const searchNumber = Number(value);

    this.funcionarios = this.funcionariosGeral.filter(funcionario => {
      return funcionario.nome.toLowerCase().includes(value) ||
             funcionario.rg.toLowerCase().includes(value) ||
             funcionario.id === searchNumber;
    });
  }

  // Variáveis de controle para exibição de modais e texto do botão do modal
  showModal = false;
  isEditMode: boolean = false;
  showModalExclusao: boolean = false;
  modalButtonText: string = 'Adicionar';

  // Funcionário selecionado para edição
  funcionarioSelecionado: Funcionario | null = null;
  funcionarioSelecionadoExclusao: Funcionario | null = null;

  // Abrir o modal de criação/edição de funcionário
  abrirModal(editar: boolean = false, id?: number) {
    this.isEditMode = editar;
    this.modalButtonText = editar ? 'Editar' : 'Adicionar';
  
    if (editar && id) {
      // Buscas os dados do funcionário para Editar
      this.funcionarioService.GetFuncionario(id).subscribe(response => {
        this.funcionarioSelecionado = response.dados;
        this.novoFuncionarioForm.patchValue(this.funcionarioSelecionado);
        this.fotoPreviewUrl = this.buscarImagem(this.funcionarioSelecionado.foto);
    });
    }
    // Formulário vazio para Adicionar o novo funcionario
    this.showModal = true;
  }

  // Abrir o modal de exclusão de funcionário
  abrirModalExclusao(funcionario: Funcionario) {
    this.funcionarioSelecionadoExclusao = funcionario;
    this.showModalExclusao = true;
  }

  // Fechar o modal de criação/edição de funcionário
  fecharModal() {
    this.nomeArquivo = "";
    this.showModal = false;
    this.hasFile = false;
    this.fotoPreviewUrl = null;
    this.novoFuncionarioForm = this.fb.group({
      nome: ['', Validators.required],
      rg: ['', Validators.required],
      foto: ['', null]
    });
  }

  // Processar o formulário e adicionar/editar funcionário
  submit() {
    const funcionarioData = this.novoFuncionarioForm.value;
    funcionarioData.departamentoId = this.departamentoDesejadoId;
    funcionarioData.foto = this.nomeArquivo || funcionarioData.foto || '';

    if (this.isEditMode && this.funcionarioSelecionado) {
      // Lógica para editar funcionário
      funcionarioData.id = this.funcionarioSelecionado.id;
      this.funcionarioService.PutFuncionario(funcionarioData).subscribe(
        () => {
          this.fecharModal();
          window.location.reload();
        },
        (error) => {
          console.error('Erro ao editar funcionario:', error);
        }
      );
    } else {
      // Lógica para adicionar novo funcionário
      this.funcionarioService.PostFuncionario(funcionarioData).subscribe(
        () => {
          this.fecharModal();
          window.location.reload();
        },
        (error) => {
          console.error('Erro ao adicionar funcionario:', error);
        }
      );
    }
  }

  // Processar a exclusão de funcionário
  submitExclusao() {
    if (this.funcionarioSelecionadoExclusao && this.funcionarioSelecionadoExclusao.id) {
      if (this.funcionarioSelecionadoExclusao.foto){
        this.funcionarioSelecionadoExclusao.foto = "";
      }
      
      this.funcionarioService.DeleteFuncionario(this.funcionarioSelecionadoExclusao.id).subscribe(
        () => {
          console.log('Funcionário excluído com sucesso.');
          window.location.reload();
        },
        (error) => {
          console.error('Erro ao excluir funcionario:', error);
        });
    } else {
      console.error('Nenhum funcionário selecionado para exclusão.');
    }
  }

  // Fechar o modal de exclusão de funcionário
  fecharModalExclusao() {
    this.showModalExclusao = false;
  }

  // Buscar a URL da imagem do funcionário
  buscarImagem(nomeDaImagem: any): any {
    if (nomeDaImagem) {
      return this.funcionarioService.apiUrlI+"/"+nomeDaImagem;
    }
  }

}
