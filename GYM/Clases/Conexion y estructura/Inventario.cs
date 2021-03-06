﻿using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace GYM.Clases
{
    class Inventario
    {
        #region Propiedades
        private int id;
        private int idProducto;
        private int cantidad;
        private decimal precio;
        private int createUser;
        private DateTime createTime;
        private int updateUser;
        private DateTime updateTime;

        


        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public int IDProducto
        {
            get { return idProducto; }
            set { idProducto = value; }
        }

        public int IDSucursal
        {
            get { return idSucursal; }
            set { idSucursal = value; }
        }

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        public decimal PrecioMedioMayoreo
        {
            get { return precio; }
            set { precio = value; }
        }

        public decimal Precio2
        {
            get { return precio2; }
            set { precio2 = value; }
        }

        public decimal PrecioEspecial
        {
            get { return precioEspecial; }
            set { precioEspecial = value; }
        }

        public int CreateUser
        {
            get { return createUser; }
            set { createUser = value; }
        }

        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        public int UpdateUser
        {
            get { return updateUser; }
            set { updateUser = value; }
        }


       
        
        #endregion

        public Inventario()
        {

        }

        /// <summary>
        /// Inicializa la instancia de la clase Inventario con el id de producto y registra el ID del inventario
        /// </summary>
        /// <param name="idProducto">ID del producto con el que está relacionado el inventario</param>
        /// <exception cref="MySql.Data.MySqlClient.MySqlException"></exception>
        /// <exception cref="System.Exception"></exception>
        public Inventario(int idProducto)
        {
            IDProducto = idProducto;
            ID = ObtenerIDInventario();
        }

        private int ObtenerIDInventario()
        {
            int id = 0;
            try
            {
                MySqlCommand sql = new MySqlCommand();
                sql.CommandText = "SELECT id FROM inventario WHERE id_producto=?id_producto";
                sql.Parameters.AddWithValue("?id_producto", idProducto);
                DataTable dt = ConexionBD.EjecutarConsultaSelect(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    id = (int)dr["id"];
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public Inventario(int id)
        {
            ID = id;
        }

        public void ObtenerDatos()
        {
            try
            {
                MySqlCommand sql = new MySqlCommand();
                sql.CommandText = "SELECT * FROM inventario WHERE id=?id";
                sql.Parameters.AddWithValue("?id", id);
                DataTable dt = ConexionBD.EjecutarConsultaSelect(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    idProducto = (int)dr["id_producto"];
                    cantidad = (int)dr["cant"];
                    precio = (decimal)dr["precio"];
                    createUser = (int)dr["create_user"];
                    createTime = (DateTime)dr["create_time"];
                    if (dr["update_user"] != DBNull.Value)
                        updateUser = (int)dr["update_user"];
                    else
                        updateUser = 0;
                    if (dr["update_time"] != DBNull.Value)
                        updateTime = (DateTime)dr["update_time"];
                    else
                        updateTime = new DateTime();
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insertar()
        {
            try
            {
                MySqlCommand sql = new MySqlCommand();
                sql.CommandText = "INSERT INTO inventario (id_producto, cant, precio, create_user, create_time) " +
                    "VALUES (?id_producto, ?id_sucursal, ?cant, ?precio, ?create_user, NOW())";
                sql.Parameters.AddWithValue("?id_producto", idProducto);
                sql.Parameters.AddWithValue("?cant", cantidad);
                sql.Parameters.AddWithValue("?precio", precio);
                sql.Parameters.AddWithValue("?create_user", Usuario.IDUsuarioActual);
                //this.id = ConexionBD.EjecutarConsulta(sql);
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Editar()
        {
            try
            {
                MySqlCommand sql = new MySqlCommand();
                sql.CommandText = "UPDATE inventario SET id_producto=?id_producto, id_sucursal=?id_sucursal, cant=?cant, " + 
                    "precio=?precio,  update_user=?update_user, update_time=NOW() WHERE id=?id";
                sql.Parameters.AddWithValue("?id_producto", idProducto);
                sql.Parameters.AddWithValue("?id_sucursal", idSucursal);
                sql.Parameters.AddWithValue("?cant", cantidad);
                sql.Parameters.AddWithValue("?precio", precio);
                sql.Parameters.AddWithValue("?update_user", Usuario.IDUsuarioActual);
                sql.Parameters.AddWithValue("?id", id);
                ConexionBD.EjecutarConsulta(sql);
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método que suma la cantidad existente de un producto en inventario con la cantidad dada
        /// </summary>
        /// <param name="id">ID del producto</param>
        /// <param name="cant">Cantidad a sumar (Para restar ingresar un número negativo)</param>
        public static void CambiarCantidadInventario(int id, int cant)
        {
            try
            {
                MySqlCommand sql = new MySqlCommand();
                sql.CommandText = "UPDATE inventario SET cant=cant+?cant WHERE id_producto=?id";
                sql.Parameters.AddWithValue("?cant", cant);
                sql.Parameters.AddWithValue("?id", id);
                ConexionBD.EjecutarConsulta(sql);
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public void ModificarCantidadInventario(int id, int cant, int idSuc)
        {
            try
            {
                MySqlCommand sql = new MySqlCommand();
                sql.CommandText = "UPDATE inventario SET cant=?cant WHERE id_producto=?id";
                sql.Parameters.AddWithValue("?cant", cant);
                sql.Parameters.AddWithValue("?id", id);
                ConexionBD.EjecutarConsulta(sql);
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ReiniciarInventario()
        {
            try
            {
                MySqlCommand sql = new MySqlCommand();
                sql.CommandText = "UPDATE inventario SET cant=0";
                ConexionBD.EjecutarConsulta(sql);
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static int CantidadProducto(int id)
        {
            int cant = 0;
            try
            {
                MySqlCommand sql = new MySqlCommand();
                sql.CommandText = "SELECT cant FROM inventario WHERE id_producto=?id";
                sql.Parameters.AddWithValue("?id", id);
                DataTable dt = ConexionBD.EjecutarConsultaSelect(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    cant = (int)dr["cant"];
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cant;
        }

        public static decimal[] PrecioProducto(int id)
        {
            decimal[] precio = new decimal[3] { 0M, 0M, 0M };
            try
            {
                MySqlCommand sql = new MySqlCommand();
                sql.CommandText = "SELECT precio FROM inventario WHERE id=?id";
                sql.Parameters.AddWithValue("?id", id);
                DataTable dt = ConexionBD.EjecutarConsultaSelect(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    precio[1] = (decimal)dr["precio"];
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return precio;
        }

        public static bool TieneInventario(int idProducto)
        {
            bool tiene = false;
            try
            {
                MySqlCommand sql = new MySqlCommand();
                sql.CommandText = "SELECT id FROM inventario WHERE id_producto=?id_producto";
                sql.Parameters.AddWithValue("?id_producto", idProducto);
                DataTable dt = ConexionBD.EjecutarConsultaSelect(sql);
                if (dt.Rows.Count > 0)
                {
                    tiene = true;
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tiene;
        }
    }
}
