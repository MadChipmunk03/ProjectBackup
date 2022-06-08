import { CompilerConfig } from '@angular/compiler';
import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ConfigPanelComponent } from './admin-panel/main-panel/config-panel/config-panel.component';
import { ConfigsListComponent } from './admin-panel/main-panel/configs-list/configs-list.component';
import { DeamonPanelComponent } from './admin-panel/main-panel/deamon-panel/deamon-panel.component';
import { DeamonsListComponent } from './admin-panel/main-panel/deamons-list/deamons-list.component';
import { ServerPanelComponent } from './admin-panel/main-panel/server-panel/server-panel.component';
import { StatsComponent } from './admin-panel/main-panel/stats/stats.component';
import { LoginComponent } from './admin-panel/login/login.component';
import { NotFoundComponent } from './admin-panel/not-found/not-found.component';
import { AuthGuard } from './services/auth.guard';

const routes: Routes = [
  { path: 'login', component: LoginComponent },

  { path: '', component: ServerPanelComponent, canActivate: [AuthGuard] },
  { path: 'configs', component: ConfigsListComponent, canActivate: [AuthGuard] },
  { path: 'config/:id', component: ConfigPanelComponent, canActivate: [AuthGuard] },
  { path: 'config/:id/:destid', component: ConfigPanelComponent, canActivate: [AuthGuard] },
  { path: 'deamons', component: DeamonsListComponent, canActivate: [AuthGuard] },
  { path: 'deamon/:id', component: DeamonPanelComponent, canActivate: [AuthGuard] },
  { path: 'stats', component: StatsComponent, canActivate: [AuthGuard] },
  { path: '**', component: NotFoundComponent, canActivate: [AuthGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
