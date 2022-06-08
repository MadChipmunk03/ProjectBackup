import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DialoguesComponent } from './dialogues/dialogues.component';
import { OverlayComponent } from './admin-panel/overlay/overlay.component';
import { LoginComponent } from './admin-panel/login/login.component';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { TopPanelComponent } from './admin-panel/top-panel/top-panel.component';
import { SidePanelComponent } from './admin-panel/side-panel/side-panel.component';
import { MainPanelComponent } from './admin-panel/main-panel/main-panel.component';
import { ServerPanelComponent } from './admin-panel/main-panel/server-panel/server-panel.component';
import { ConfigPanelComponent } from './admin-panel/main-panel/config-panel/config-panel.component';
import { DeamonPanelComponent } from './admin-panel/main-panel/deamon-panel/deamon-panel.component';
import { NewBackupComponent } from './admin-panel/main-panel/configs-list/dialogues/new-backup/new-backup.component';
import { FiltersComponent } from './dialogues/filters/filters.component';
import { AddDestinationComponent } from './dialogues/add-destination/add-destination.component';
import { ConfigsListComponent } from './admin-panel/main-panel/configs-list/configs-list.component';
import { DeamonsListComponent } from './admin-panel/main-panel/deamons-list/deamons-list.component';
import { StatsComponent } from './admin-panel/main-panel/stats/stats.component';
import { StatsGeneralComponent } from './admin-panel/main-panel/stats/components/stats-general/stats-general.component';
import { StatsEventsComponent } from './admin-panel/main-panel/stats/components/stats-events/stats-events.component';
import { NotFoundComponent } from './admin-panel/not-found/not-found.component';
import { DiaConfigsFilterComponent } from './admin-panel/main-panel/configs-list/dialogues/dia-configs-filter/dia-configs-filter.component';
import { DiaEventsFilterComponent } from './admin-panel/main-panel/stats/dia-events-filter/dia-events-filter.component';
import { DiaDeamonsFilterComponent } from './admin-panel/main-panel/deamons-list/dia-deamons-filter/dia-deamons-filter.component';
import { ListConfConnectionsComponent } from './admin-panel/main-panel/config-panel/components/list-conf-connections/list-conf-connections.component';
import { EditConfComponent } from './admin-panel/main-panel/config-panel/components/edit-conf/edit-conf.component';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';
import { LoadingPageComponent } from './admin-panel/main-panel/loading-page/loading-page.component';
import { DiaDestinationComponent } from './admin-panel/main-panel/config-panel/components/dia-destination/dia-destination.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';

function tokenGetter() {
  console.log('xxx');
  return sessionStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    DialoguesComponent,
    OverlayComponent,
    LoginComponent,
    AdminPanelComponent,
    TopPanelComponent,
    SidePanelComponent,
    MainPanelComponent,
    ServerPanelComponent,
    ConfigPanelComponent,
    DeamonPanelComponent,
    NewBackupComponent,
    FiltersComponent,
    AddDestinationComponent,
    ConfigsListComponent,
    DeamonsListComponent,
    StatsComponent,
    StatsGeneralComponent,
    StatsEventsComponent,
    NotFoundComponent,
    DiaConfigsFilterComponent,
    DiaEventsFilterComponent,
    DiaDeamonsFilterComponent,
    ListConfConnectionsComponent,
    EditConfComponent,
    LoadingPageComponent,
    DiaDestinationComponent,
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatCheckboxModule,
    MatChipsModule,
    AppRoutingModule,
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
      },
    }),
    BrowserAnimationsModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
