﻿/* 
 * Copyright (C) 2014 Mehdi El Gueddari
 * http://mehdi.me
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 */
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.DbContextScope.NetCore
{
    public class AmbientDbContextLocator : IAmbientDbContextLocator {

        public TDbContext Get<TDbContext>() where TDbContext : DbContext
        {
            var ambientDbContextScope = DbContextScope.GetAmbientScope();
            return ambientDbContextScope?.DbContexts.Get<TDbContext>();
        }

        public TDbContext Get<TDbContext, TIdentity>(TIdentity identity) where TDbContext : DbContext
        {
            var ambientDbContextScope = DbContextScope.GetAmbientScope();
            return ambientDbContextScope?.DbContexts.Get<TDbContext, TIdentity>(identity);
        }
    }
}