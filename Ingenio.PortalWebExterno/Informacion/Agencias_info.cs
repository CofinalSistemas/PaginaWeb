using Ingenio.PortalWebExterno.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ingenio.PortalWebExterno.Informacion
{
    public class Agencias_info
    {
        public List<Agencia> ObtenerAgencias()
        {
            return new List<Agencia>()
            {
                new Agencia()
                {
                    Id = "Pasto",
                    Nombre = "PASTO",
                    Direcion = "Cra 29 No 18–41",
                    Telefono = 7336300,
                    Extensiones = "100,101,102",
                    Url = "https://www.cideu.org/wp-content/uploads/Pasto5.jpg",
                    Departamento = "Nariño",
                    Mapas = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3988.9161435617775!2d-77.28228828476834!3d1.2184706623217287!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x8e2ed487375d6129%3A0x75d762a9f2c9e3da!2sCOFINAL!5e0!3m2!1ses!2sco!4v1669234761790!5m2!1ses!2sco"
                },
                new Agencia()
                {
                    Id = "Sandona",
                    Nombre = "SANDONA",
                    Direcion = "Calle 5ta. No. 03-77",
                    Telefono = 7336300,
                    Extensiones = "200 - 201",
                    Url = "http://www.cafeoccidente.co/images/paisajes/sandona.jpg",
                    Departamento = "Nariño",
                    Mapas = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3988.8138929143192!2d-77.47284868476852!3d1.2856698621352562!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x8e2ec932ca64d879%3A0x9aed4f16983e6b61!2zQ2wuIDUgIzMsIFNhbmRvbsOhLCBOYXJpw7Fv!5e0!3m2!1ses!2sco!4v1669234873509!5m2!1ses!2sco",
                },
                new Agencia()
                {
                    Id = "Ipiales",
                    Nombre = "IPIALES",
                    Direcion = "Calle 16 No 7-40",
                    Telefono = 7336300,
                    Extensiones = "300 - 301",
                    Url = "https://situr.narino.gov.co/storage/Clientes/situr_narino/principal/imagenes/contenidos/19-19_Parque_20_de_Julio_y_Catedral_de_Ipiales.JPG",
                    Departamento = "Nariño",
                    Mapas = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3989.404036355164!2d-77.64410608476729!3d0.8256840632640937!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x8e296be89c46e063%3A0x51408567722c47df!2sCOFINAL!5e0!3m2!1ses!2sco!4v1669234931107!5m2!1ses!2sco",
                },
                new Agencia()
                {
                    Id = "Consaca",
                    Nombre = "CONSACÁ",
                    Direcion = "Carrera 7 No 2-61",
                    Telefono = 7336300,
                    Extensiones = "400 - 401",
                    Url = "https://cdn.municipios.com.co/fotos/755-2017-11-25-17-06-913-L.jpg",
                    Departamento = "Nariño",
                    Mapas = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3988.933285924464!2d-77.46941728476835!3d1.2068385623532416!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x8e2ecc9e872f0c7d%3A0x7ffc9c60efda9253!2zQ2FycmVyYSA3wqosIENvbnNhY2EsIE5hcmnDsW8!5e0!3m2!1ses!2sco!4v1669235096873!5m2!1ses!2sco",
                },
                new Agencia()
                {
                    Id = "Buesaco",
                    Nombre = "BUESACO",
                    Direcion = "Carrera 3 No 8-22",
                    Telefono = 7336300,
                    Extensiones = "500 - 501",
                    Url = "https://colombiaextraordinaria.com/somos_colombia/external/img/img_departamentos/Buesacoimagen_ce.jpg",
                    Departamento = "Nariño",
                    Mapas = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3988.653341869112!2d-77.1591754847688!3d1.384619961847278!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x8e2f27197bfea87d%3A0xbd864020c9e32eb7!2sCra.%203%20%238-22%2C%20Buesaco%2C%20Nari%C3%B1o!5e0!3m2!1ses!2sco!4v1669235158748!5m2!1ses!2sco",
                },
                new Agencia()
                {
                    Id = "Taminango",
                    Nombre = "TAMINANGO",
                    Direcion = "Calle 3 No. 4 -70 B/San Francisco",
                    Telefono = 7336300,
                    Extensiones = "600 - 601",
                    Url = "https://www.viajarenverano.com/wp-content/uploads/2017/01/LaCalidosaFM.png",
                    Departamento = "Nariño",
                    Mapas = "https://www.google.com/maps/embed?pb=!1m16!1m12!1m3!1d1994.160456657324!2d-77.28174838595196!3d1.5698009498953795!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!2m1!1sTaminango%2C%20Nari%C3%B1o%20calle%203!5e0!3m2!1ses!2sco!4v1669235363619!5m2!1ses!2sco"

                },
                new Agencia()
                {
                    Id = "Union",
                    Nombre = "LA UNION",
                    Direcion = "Carrera 2a No 17-67 B/Eduardo Santos",
                    Telefono = 7336300,
                    Extensiones = "700 - 701",
                    Url = "https://2.bp.blogspot.com/-mvbWdO5axAM/WB0pINKgccI/AAAAAAAAAFg/T7BNIMT4imUtvdT7PrOaNje65lFUYUKMACEw/s1600/Iglesia_Ntra._Sra._del_Rosario.JPG",
                    Departamento = "Nariño",
                    Mapas="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3987.975116589176!2d-77.13394998474487!3d1.6047439611492522!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x8e2f1cf1f0fd732d%3A0x6428b939481739f3!2zQ2FycmVyYSAywqogIzE3LCBMYSBVbmnDs24sIE5hcmnDsW8!5e0!3m2!1ses!2sco!4v1669235499874!5m2!1ses!2sco",
                },
                new Agencia()
                {
                    Id = "Tumaco",
                    Nombre = "TUMACO",
                    Direcion = "Carrera 2a No 17-67 B/Eduardo Santos",
                    Telefono = 7336300,
                    Extensiones = "700 - 701",
                    Url = "https://situr.narino.gov.co/storage/Clientes/situr_narino/principal/imagenes/contenidos/1063-31_Pen%CC%83a_El_Morro_y_Kioscos.jpg",
                    Departamento = "Nariño",
                    Mapas = "",
                },
                new Agencia()
                {
                    Id = "Linares",
                    Nombre = "LINARES",
                    Direcion = "Cra 29 No 18–41",
                    Telefono = 7336300,
                    Extensiones = "100,101,102",
                    Url = "https://upload.wikimedia.org/wikipedia/commons/a/ae/Linares_%28plaza_principal%29.png",
                    Departamento = "Nariño",
                    Mapas = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d838.522893830635!2d-77.52390522205192!3d1.3507931361316698!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x8e2ec7c9a05781b5%3A0xa55e27c5f4f26bdc!2zQ2wuIDTCqiwgTGluYXJlcywgTmFyacOxbw!5e0!3m2!1ses!2sco!4v1669235821172!5m2!1ses!2sco",
                },
                new Agencia()
                {
                    Id = "Ancuya",
                    Nombre = "ANCUYA",
                    Direcion = "Calle 5ta. No. 03-77",
                    Telefono = 7336300,
                    Extensiones = "200 - 201",
                    Url = "https://www.viajarenverano.com/wp-content/uploads/2016/05/FrontisAncu.png",
                    Departamento = "Nariño",
                    Mapas = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d1677.1043793928588!2d-77.51451240153587!3d1.2630518819039231!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x8e2ec97f28a967e5%3A0xbf97635abe8736a3!2sCl.%202%20%234-27%2C%20Ancuya%2C%20Nari%C3%B1o!5e0!3m2!1ses!2sco!4v1669235914385!5m2!1ses!2sco",
                },
                new Agencia()
                {
                    Id = "Tuquerres",
                    Nombre = "TUQUERRES",
                    Direcion = "Calle 16 No 7-40",
                    Telefono = 7336300,
                    Extensiones = "300 - 301",
                    Url = "https://fastly.4sqi.net/img/general/600x600/45094903_5f8aHbK2KictpAWNY39eSSAQdU6DFDkTPPs95riZc9o.jpg",
                    Departamento = "Nariño",
                    Mapas = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3989.099881992051!2d-77.61937546899317!3d1.087332429444868!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x8e29491277835427%3A0x55cf8b7aee85c3ad!2zQ2wuIDE3ICMxMi01NCwgVMO6cXVlcnJlcywgTmFyacOxbw!5e0!3m2!1ses!2sco!4v1669235979195!5m2!1ses!2sco",
                },
                new Agencia()
                {
                    Id = "Gualmatan",
                    Nombre = "GUALMATAN",
                    Direcion = "Carrera 7 No 2-61",
                    Telefono = 7336300,
                    Extensiones = "400 - 401",
                    Url = "https://3.bp.blogspot.com/-wBi8xhRVNro/WIydRRZ7w3I/AAAAAAABBAU/baie-UrzlYw197DNh39cdKtpXL0h9jBHgCLcB/s1600/01-TEMPLO%2BPARROQUIAL-GUALMATAN.jpg",
                    Departamento = "Nariño",
                    Mapas = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d1653.854119553033!2d-77.5678618502578!3d0.9200968895710606!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x8e29407dd0e04495%3A0xfb8ade559724401a!2zQ2wuIDUgIzUtMjgsIEd1YWxtYXTDoW4sIE5hcmnDsW8!5e0!3m2!1ses!2sco!4v1669236105062!5m2!1ses!2sco",
                },
                new Agencia()
                {
                    Id = "Guachucal",
                    Nombre = "GUACHUCAL",
                    Direcion = "Carrera 3 No 8-22",
                    Telefono = 7336300,
                    Extensiones = "500 - 501",
                    Url = "https://radionacional-v3.s3.amazonaws.com/s3fs-public/senalradio/articulo-noticia/galeriaimagen/guachucal_plaza_-_copia.jpeg",
                    Departamento = "Nariño",
                    Mapas = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3989.258160927812!2d-77.73469338474307!3d0.9601121629698804!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x8e2944a7f2f8c0fb%3A0x61b327cd2ccff776!2sCra.%205%20%234-67%2C%20Guachucal%2C%20Nari%C3%B1o!5e0!3m2!1ses!2sco!4v1669236154002!5m2!1ses!2sco",
                },
                new Agencia()
                {
                    Id = "Tambo",
                    Nombre = "EL TAMBO",
                    Direcion = "Calle 3 No. 4 -70 B/San Francisco",
                    Telefono = 7336300,
                    Extensiones = "600 - 601",
                    Url = "https://www.viajarenverano.com/wp-content/uploads/2021/09/El-Tambo-Frontis1.jpg",
                    Departamento = "Nariño",
                    Mapas = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3988.6117730608703!2d-77.39659948255616!3d1.409107700000005!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x8e2ec358a2cc4693%3A0xae21fd752f6ba316!2sCra.%2010%20%232-33%2C%20El%20Tambo%2C%20Nari%C3%B1o!5e0!3m2!1ses!2sco!4v1669236235950!5m2!1ses!2sco",
                },
                new Agencia()
                {
                    Id = "Spablo",
                    Nombre = "SAN PABLO",
                    Direcion = "Carrera 2a No 17-67 B/Eduardo Santos",
                    Telefono = 7336300,
                    Extensiones = "700 - 701",
                    Url = "https://situr.narino.gov.co/storage/Clientes/situr_narino/principal/imagenes/contenidos/2195-2_Santuario_Nuestra_Se%C3%B1ora_de_la_Playa.jpg",
                    Departamento = "Nariño",
                    Mapas = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3988.1264753765354!2d-77.01289618474492!3d1.6685593609321203!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x8e2f057e2b851bad%3A0x43731d51da7e38c1!2sCl.%205%20%232-23%2C%20San%20Pablo%2C%20Nari%C3%B1o!5e0!3m2!1ses!2sco!4v1669236537187!5m2!1ses!2sco",
                },
                new Agencia()
                {
                    Id = "Lorenzo",
                    Nombre = "LORENZO DE ALDANA - PASTO",
                    Direcion = "Carrera 2a No 17-67 B/Eduardo Santos",
                    Telefono = 7336300,
                    Extensiones = "700 - 701",
                    Url = "https://situr.narino.gov.co/storage/Clientes/situr_narino/principal/imagenes/contenidos/4892-1_Parque_20_de_Julio_y_Catedral_de_Ipiales.jpg",
                    Departamento = "Nariño",
                    Mapas = "https://www.google.com/maps/embed?pb=!1m23!1m12!1m3!1d997.2365004386402!2d-77.26569951100004!3d1.198137184284337!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!4m8!3e6!4m0!4m5!1s0x8e2ed4bf0fd7e989%3A0x5dfa087a4ad24ea8!2sCl.%2017d%20%231-2%20a%201-54%2C%20Pasto%2C%20Nari%C3%B1o!3m2!1d1.1981206!2d-77.2656914!5e0!3m2!1ses!2sco!4v1669237034345!5m2!1ses!2sco",
                },
                new Agencia()
                {
                    Id = "Alban",
                    Nombre = "SAN JOSE DE ALBAN",
                    Direcion = "Carrera 7 No 2-61",
                    Telefono = 7336300,
                    Extensiones = "400 - 401",
                    Url = "https://www.viajarenverano.com/wp-content/uploads/2020/03/Alban-Iglesia.jpg",
                    Departamento = "Nariño",
                    Mapas = "",
                },
                new Agencia()
                {
                    Id = "Slorenzo",
                    Nombre = "SAN LORENZO",
                    Direcion = "Carrera 3 No 8-22",
                    Telefono = 7336300,
                    Extensiones = "500 - 501",
                    Url = "https://www.viajarenverano.com/wp-content/uploads/2021/11/San-Lorenzo-Centro.jpg",
                    Departamento = "Nariño",
                },
                new Agencia()
                {
                    Id = "Cumbal",
                    Nombre = "CUMBAL",
                    Direcion = "Calle 3 No. 4 -70 B/San Francisco",
                    Telefono = 7336300,
                    Extensiones = "600 - 601",
                    Url = "https://media-cdn.tripadvisor.com/media/photo-s/19/f4/1f/17/cumbal-es-un-municipio.jpg",
                    Departamento = "Nariño",
                },
                new Agencia()
                {
                    Id = "Bernardo",
                    Nombre = "SAN BERNARDO",
                    Direcion = "Carrera 2a No 17-67 B/Eduardo Santos",
                    Telefono = 7336300,
                    Extensiones = "700 - 701",
                    Url = "https://www.colombiaturismoweb.com/DEPARTAMENTOS/NARINO/MUNICIPIOS/SAN%20BERNARDO/imagenes/DSC06427_thumb_1.JPG",
                    Departamento = "Nariño",
                },
                new Agencia()
                {
                    Id = "Penol",
                    Nombre = "EL PEÑOL",
                    Direcion = "Carrera 2a No 17-67 B/Eduardo Santos",
                    Telefono = 7336300,
                    Extensiones = "700 - 701",
                    Url = "https://www.viajarenverano.com/wp-content/uploads/2019/10/El-Pe%C3%B1ol-Parque.jpg",
                    Departamento = "Nariño",
                },
                new Agencia()
                {
                    Id = "Florida",
                    Nombre = "LA FLORIDA",
                    Direcion = "Calle 3 No. 4 -70 B/San Francisco",
                    Telefono = 7336300,
                    Extensiones = "600 - 601",
                    Url = "https://2.bp.blogspot.com/-qzSyN_p_Cak/VTmpcUP0XHI/AAAAAAAAABk/6jEWkG6zqcE/s1600/Imagen1.jpg",
                    Departamento = "Nariño",
                    Mapas = "",
                },
                new Agencia()
                {
                    Id = "Narino",
                    Nombre = "NARIÑO",
                    Direcion = "Carrera 2a No 17-67 B/Eduardo Santos",
                    Telefono = 7336300,
                    Extensiones = "700 - 701",
                    Url = "https://www.viajarenverano.com/wp-content/uploads/2019/12/Nari%C3%B1o-Frontis.jpg",
                    Departamento = "Nariño",
                    Mapas="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d1410.2565577488388!2d-77.35889566926097!3d1.2894992900044897!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x8e2ed1b1ae1823ef%3A0xfb5c04ec38b4ca36!2sNarino%2C%20Pasto%2C%20Nari%C3%B1o!5e0!3m2!1ses!2sco!4v1669240476227!5m2!1ses!2sco",
                },
                new Agencia()
                {
                    Id = "Samaniego",
                    Nombre = "SAMANIEGO",
                    Direcion = "Carrera 2a No 17-67 B/Eduardo Santos",
                    Telefono = 7336300,
                    Extensiones = "700 - 701",
                    Url = "https://www.elcolombiano.com/documents/10157/0/580x435/0c13/580d365/none/11101/INYJ/image_content_36215433_20200816094315.jpg",
                    Departamento = "Nariño",
                    Mapas = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d419.2634810888939!2d-77.59326657537592!3d1.3389521913981821!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x8e2eb8269b2ccd87%3A0xab862ed51520859e!2sCra.%203%2C%20Samaniego%2C%20Nari%C3%B1o!5e0!3m2!1ses!2sco!4v1669238242963!5m2!1ses!2sco",
                },
                new Agencia()
                {
                    Id = "Cruz",
                    Nombre = "LA CRUZ",
                    Direcion = "Carrera 7 No 2-61",
                    Telefono = 7336300,
                    Extensiones = "400 - 401",
                    Url = "https://www.colombiaturismoweb.com/DEPARTAMENTOS/NARINO/MUNICIPIOS/LA%20CRUZ/imagenes/Iglecia_nuestra_Se_ora_del_Carmen_thumb_1.jpg",
                    Departamento = "Nariño",
                    Mapas = "https://www.google.com/maps/embed?pb=!1m14!1m12!1m3!1d403.8180526485087!2d-76.97148857921893!3d1.6002836029583498!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!5e0!3m2!1ses!2sco!4v1669238032743!5m2!1ses!2sco"
                },
                new Agencia()
                {
                    Id = "Chachagui",
                    Nombre = "CHACHAGUI",
                    Direcion = "Carrera 3 No 8-22",
                    Telefono = 7336300,
                    Extensiones = "500 - 501",
                    Url = "https://www.udenar.edu.co/recursos/wp-content/uploads/2018/12/ultimos-artesanos-cafeteros-de-guapuiy-chachagui-4.jpg",
                    Departamento = "Nariño",
                    Mapas = "https://www.google.com/maps/embed?pb=!1m16!1m12!1m3!1d997.1735653931095!2d-77.28428532690096!3d1.360084139675478!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!2m1!1sChachag%C3%BC%C3%AD%2C%20Narino%20calle%203%20No.%208%20-07!5e0!3m2!1ses!2sco!4v1669237815138!5m2!1ses!2sco"
                },
                new Agencia()
                {
                    Id = "Popayan",
                    Nombre = "POPAYÁN",
                    Direcion = "Calle 4 No 4-12 Ed.Altozano Centro Histórico",
                    Telefono = 7336300,
                    Extensiones = "900 - 901",
                    Url = "https://upload.wikimedia.org/wikipedia/commons/1/1a/Catedralpopayan.jpg",
                    Departamento = "Cauca",
                },
                new Agencia()
                {
                    Id = "Mercaderes",
                    Nombre = "MERCADERES ",
                    Direcion = "Carrera 3 No 8-22",
                    Telefono = 7336300,
                    Extensiones = "500 - 501",
                    Url = "https://www.eltiempo.com/files/article_content/uploads/2018/10/26/5bd3ba4927370.jpeg",
                    Departamento = "Cauca",
                },
                new Agencia()
                {
                    Id = "Sibundoy",
                    Nombre = "SIBUNDOY",
                    Direcion = "Calle 15 No 15-74 B/Oriental",
                    Telefono = 7336300,
                    Extensiones = "120 - 121",
                    Url = "https://www.eltiempo.com/files/article_content/uploads/2018/10/26/5bd3ba4927370.jpeg",
                    Departamento = "Putumayo",
                },
                new Agencia()
                {
                    Id = "Palmira",
                    Nombre = "PALMIRA ",
                    Direcion = "Calle 29 No 29-39 Centro - Respaldo de Nuestra Señora del Palmar",
                    Telefono = 7336300,
                    Extensiones = "202 - 203",
                    Url = "https://caracoltv.brightspotcdn.com/dims4/default/39d8175/2147483647/strip/true/crop/1000x566+0+0/resize/1000x566!/quality/90/?url=http%3A%2F%2Fcaracol-brightspot.s3.amazonaws.com%2Ffb%2F28%2F5d677744438d8427422e73e4102b%2Fpalmira-valle-del-cauca.jpg",
                    Departamento = "Valle del Cauca",
                },
            };
        }
    }
}