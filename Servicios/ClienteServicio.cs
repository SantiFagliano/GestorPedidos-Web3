﻿using Microsoft.EntityFrameworkCore;
using Models.DTO;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class ClienteServicio : IClienteServicio
    {

        private _20211CTPContext _dbContext;
        private IPedidoServicio pedidoServicio;

        public ClienteServicio(_20211CTPContext dbContext)
        {
            this.pedidoServicio = new PedidoServicio(dbContext);
            _dbContext = dbContext;
        }

        public void Crear(Cliente entity, int idUsuario)
        {
            _dbContext.Clientes.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Borrar(Cliente entity, int idUsuario)
        {
            throw new NotImplementedException();
        }

        public void BorrarPorId(int id, int idUsuario)
        {
            Cliente objActual = ObtenerPorId(id);
            objActual.FechaBorrado = DateTime.Now;
            _dbContext.SaveChanges();
        }

        public List<Cliente> ListarTodos()
        {
            return _dbContext.Clientes.ToList();
        }

        public Cliente ObtenerPorId(int id)
        {
            return _dbContext.Clientes
                .FirstOrDefault(o => o.IdCliente == id);
        }

        public void Modificar(Cliente entity, int idUsuario)
        {
            Cliente objActual = ObtenerPorId(entity.IdCliente);

            objActual.Nombre = entity.Nombre;
            objActual.Numero = entity.Numero;
            objActual.Telefono = entity.Telefono;
            objActual.Cuit = entity.Cuit;
            objActual.Email = entity.Email;
            objActual.Direccion = entity.Direccion;
            objActual.FechaModificacion = DateTime.Now;

            _dbContext.SaveChanges();
        }

        public List<Cliente> ListarNoEliminados()
        {
            var clientes = from a in _dbContext.Clientes
                           where a.FechaBorrado == null
                           select a;

            return clientes.ToList();
        }

        public List<Cliente> listarClientesSinPedidosActivos()
        {
            /*var clientes = from c in _dbContext.Clientes.Include(c => c.Pedidos).Include("Pedidos.IdEstadoPedido")
                           select c;

            List<Cliente> cliente = clientes.ToList();

            return cliente.ForEach(c =>
             {
                 _dbContext.Pedidos
                 .Include(p => p.IdClienteNavigation)
                 .Where(p => p.IdClienteNavigation.IdCliente == c.IdCliente && c.FechaBorrado != null)
                 .Where(p => p.IdEstado.Equals(1)).FirstOrDefault();
             }
             ).ToList();

            cliente.*/

            List<Cliente> clientesSinPedidosActivos = new List<Cliente>();
            ListarNoEliminados().ForEach(a =>
            {
                if (!this.pedidoServicio.validarSiExistePedidoAbiertoDeUnClientePorIdCliente(a.IdCliente)) clientesSinPedidosActivos.Add(a);
            });

            return clientesSinPedidosActivos;

        }

        public List<ClienteDTO> mapearListaClienteAListaClienteDTO(List<Cliente> clientes)
        {
            List<ClienteDTO> clientesDTO = new List<ClienteDTO>();
            foreach (Cliente cliente in clientes)
            {
                ClienteDTO clienteDTO = new ClienteDTO();

                clienteDTO.IdCliente = cliente.IdCliente;
                clienteDTO.Nombre = cliente.Nombre;
                clienteDTO.Numero = cliente.Numero;
                clienteDTO.Telefono = cliente.Telefono;
                clienteDTO.Direccion = cliente.Direccion;

                clientesDTO.Add(clienteDTO);
            }
            return clientesDTO;
        }
    }
}
