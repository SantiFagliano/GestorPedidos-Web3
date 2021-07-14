﻿using Models.Models;
using System.Collections.Generic;

namespace Service
{
    public interface IPedidoServicio : IBaseServicio<Pedido>
    {
        int CrearPedido(Pedido pedido);
        void CrearPedidoArticulo(PedidoArticulo entity);
        void MarcarPedidoComoCerrado(int idPedido, int idUsuario);
        void BorrarPedidosPorIdCliente(int idCliente, int idUsuario);
        void MarcarPedidoComoEntregado(int idPedido, int idUsuario);
        void EliminarArticuloAlPedido(PedidoArticulo pedidoArticulo);
        PedidoArticulo BuscarPedidoArticuloPorIdPedidoYIdArticulo(int idPedido, int idArticulo);
        Dictionary<Articulo, int> listarArticulosConCantidadesDeUnPedidoPorPedidoId(int idPedido);
        List<Articulo> listarArticulosNoSeleccionadosDeUnPedidoPorIdPedido(int idPedido);
        List<Pedido> ListarPedidosEntregados();
        List<Pedido> ListarPedidosUltimosDosMeses();
        List<PedidoArticulo> listarPedidoArticuloPorIdPedido(int idPedido);

    }
}
