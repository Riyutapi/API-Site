// Os dados e tipos das respostas da api
export interface Response<T> {
    dados: T;
    mensagem: string;
    sucesso: boolean;
}
