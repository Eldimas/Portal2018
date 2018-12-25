using System.Collections.Generic;
using System.Linq;
using PortalApp.API.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;

namespace PortalApp.API.Data
{
    public class Seed
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        private readonly DataContext _context;

        public Seed(UserManager<User> userManager, RoleManager<Role> roleManager, DataContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

         public void SeedRegions()
        {
            if (!_context.Regions.Any())
            {

                var regionData = System.IO.File.ReadAllText("Data/SeedData/RegionSeedData.json");
                var regions = JsonConvert.DeserializeObject<List<Region>>(regionData);

                foreach (var region in regions)
                {
                    region.Id = Guid.NewGuid();
                    _context.Regions.Add(region);
                }

                _context.SaveChanges();
            }
        }

        public void SeedDepartments()
        {
            if (!_context.Departments.Any())
            {
                var depData = System.IO.File.ReadAllText("Data/SeedData/DepartmentSeedData.json");
                var deps = JsonConvert.DeserializeObject<List<Department>>(depData);

                var region = _context.Regions.FirstOrDefault(x => x.NameEn == "Region 1");

                foreach (var dep in deps)
                {
                    dep.Id = Guid.NewGuid();
                    if (dep.DepartmentVs != null)
                    {
                        foreach (var depV in dep.DepartmentVs)
                        {
                            depV.Id = Guid.NewGuid();
                            depV.RegionId = region.Id;
                            depV.Created = DateTime.Now;
                        }
                    }

                    _context.Departments.Add(dep);
                }

                _context.SaveChanges();
            }
        }

        public void SeedWfProcessType()
        {
            if (!_context.WfProcessType.Any())
            {
                var processTypeData = System.IO.File.ReadAllText("Data/SeedData/WfProcessType.json");

                var processTypes = JsonConvert.DeserializeObject<List<WfProcessType>>(processTypeData);

                foreach (var proc in processTypes)
                {
                    _context.WfProcessType.Add(proc);
                }

                _context.SaveChanges();
            }
        }

        public void SeedWfProcessResult()
        {
            if (!_context.WfProcessResult.Any())
            {
                var processResData = System.IO.File.ReadAllText("Data/SeedData/WfProcessResult.json");

                var processResults = JsonConvert.DeserializeObject<List<WfProcessResult>>(processResData);

                foreach (var proc in processResults)
                {
                    _context.WfProcessResult.Add(proc);
                }

                _context.SaveChanges();
            }
        }

        public void SeedWfProcessIteration()
        {
            if (!_context.WfProcessIteration.Any())
            {
                var processIterData = System.IO.File.ReadAllText("Data/SeedData/WfProcessIteration.json");

                var processIterations= JsonConvert.DeserializeObject<List<WfProcessIteration>>(processIterData);

                foreach (var proc in processIterations)
                {
                    _context.WfProcessIteration.Add(proc);
                }

                _context.SaveChanges();
            }
        }

        public void SeedDocumentConfigs()
        {
            if (!_context.DocumentConfigs.Any())
            {
                var docConfData = System.IO.File.ReadAllText("Data/SeedData/DocumentConfigSeedData.json");
                var docConfs = JsonConvert.DeserializeObject<List<DocumentConfig>>(docConfData);

                //var region = _context.Regions.FirstOrDefault(x => x.NameEn == "Region 1");

                foreach (var doc in docConfs)
                {
                    doc.Id = Guid.NewGuid();
                    if (doc.DocumentConfigVs != null)
                    {
                        foreach (var docV in doc.DocumentConfigVs)
                        {
                            docV.Id = Guid.NewGuid();
                            //docV.DocumentConfigId = doc.Id;
                            docV.Created = DateTime.Now;
                            /* if(docV.WfConfigsSerialized != null){
                                foreach(var wfConf in docV.WfConfigsSerialized){
                                    wfConf.Id = Guid.NewGuid();

                                }
                            } */
                            if(docV.ContentConfigsSerialized != null){
                                foreach(var contentConf in docV.ContentConfigsSerialized){
                                    contentConf.Id = Guid.NewGuid();
                            
                                }
                            }
                        }
                    }

                    _context.DocumentConfigs.Add(doc);
                }

                _context.SaveChanges();
            }
        }

       

        public void SeedUsers()
        {
            // var userTempCount = _context.UserTemps.Count();

            // if (userTempCount == 0)
            //     return;
            //  if (!_userManager.Users.Any() || (_userManager.Users.Count() < userTempCount))
            if (!_userManager.Users.Any())
            {
                // var userData = System.IO.File.ReadAllText("Data/SeedData/UserSeedData.json");
                // var users = JsonConvert.DeserializeObject<List<User>>(userData);

                var roles = new List<Role>
                {
                    new Role{Name = "Member"},
                    new Role{Name = "Admin"},
                    new Role{Name = "Moderator"},
                    new Role{Name = "VIP"},
                };

                foreach (var role in roles)
                {
                    _roleManager.CreateAsync(role).Wait();
                }

                var adminUser = new User
                {
                    UserName = "Admin",
                    PhoneNumber = "+7 707 1209095",
                    Email = "admin@agp.com.kz"
                    // DepartmentVId=departmentId
                };

                IdentityResult result = _userManager.CreateAsync(adminUser, "password").Result;

                if (result.Succeeded)
                {
                    var admin = _userManager.FindByNameAsync("Admin").Result;
                    _userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" }).Wait();
                }

                // var departmentId = _context.DepartmentVs.FirstOrDefault(x => x.Name == "DepartmentV_2_2").Id;

                var userTemps = _context.UserTemps.ToList();

                List<User> users = new List<User>();

                foreach (var userTemp in userTemps)
                {
                    var isExist = _context.Users.FirstOrDefault(x => x.UserName == userTemp.Name);

                    if (isExist == null)
                    {
                        User user = new User()
                        {
                            UserName = userTemp.Name,
                            PhoneNumber = userTemp.Phone,
                            Email = userTemp.Email,

                        };
                        users.Add(user);
                    }



                }

                foreach (var user in users)
                {
                    // user.DepartmentVId=departmentId;
                    _userManager.CreateAsync(user, "password").Wait();
                    _userManager.AddToRoleAsync(user, "Member").Wait();


                }

                
            }
        }

        public void SeedUserV()
        {
            if (!_context.UserVs.Any())
            {

                var users = _context.Users.ToList();
                foreach (var user in users)
                {
                    var userTemp = _context.UserTemps.FirstOrDefault(x => x.Name == user.UserName);

                    if (userTemp != null)
                    {
                        var departmentId = Guid.Parse("0ABABC65-69D2-4854-A0AD-07402FE6462C");
                        var dep = _context.Departments.FirstOrDefault(x => x.KeyIndex == userTemp.DepartmentName);

                        if (dep != null)
                        {
                            var depV = _context.DepartmentVs.FirstOrDefault(x => x.DepartmentId == dep.Id);
                            if (depV != null)
                            {
                                departmentId = depV.Id;
                            }
                        }else {
                            departmentId = _context.DepartmentVs.FirstOrDefault().Id;
                        }


                        var userv = new UserV()
                        {
                            Id = Guid.NewGuid(),
                            Created = DateTime.Now,
                            DisplayNameRus = userTemp.DisplayNameRus,
                            DisplayNameEng = userTemp.DisplayNameEng,
                            DisplayNameKaz = userTemp.DisplayNameKaz,

                            FromNameRus = userTemp.FromNameRus,
                            FromNameEng = userTemp.FromNameEng,
                            FromNameKaz = userTemp.FromNameKaz,

                            ToNameRus = userTemp.ToNameRus,
                            ToNameEng = userTemp.ToNameEng,
                            ToNameKaz = userTemp.ToNameKaz,

                            Priority = 0,
                            Disabled = false,
                            UserId = user.Id,
                            DepartmentVId = departmentId

                        };
                        _context.UserVs.Add(userv);
                    }


                }

                _context.SaveChanges();
            }
        }
    }
}