import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { JumbotronComponent } from './components/jumbotron/jumbotron.component';
import { SidenavComponent } from '../../shared/components/sidenav/sidenav.component';
import { FeaturesComponent } from './components/features/features.component';
import { HttpClientModule } from '@angular/common/http';
import { PaginatorModule } from '../../shared/components/paginator/paginator.module';
import { ReactiveFormsModule } from '@angular/forms';
import { NavbarModule } from '../../shared/components/navbar/navbar.module';
import { SidenavModule } from '../../shared/components/sidenav/sidenav.module';



@NgModule({
  declarations: [
    HomeComponent,
    JumbotronComponent,
    FeaturesComponent,
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    PaginatorModule,
    ReactiveFormsModule,
    NavbarModule,
    SidenavModule,
  ],
  exports: [
    HomeComponent,
  ]
})
export class HomeModule { }
