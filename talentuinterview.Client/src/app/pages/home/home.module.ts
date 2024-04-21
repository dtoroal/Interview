import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { JumbotronComponent } from './components/jumbotron/jumbotron.component';
import { FeaturesComponent } from './components/features/features.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { PaginatorModule } from '../../shared/components/paginator/paginator.module';
import { ReactiveFormsModule } from '@angular/forms';
import { NavbarModule } from '../../shared/components/navbar/navbar.module';
import { SidenavModule } from '../../shared/components/sidenav/sidenav.module';
import { LoginComponent } from './components/login/login.component';



@NgModule({
  declarations: [
    HomeComponent,
    JumbotronComponent,
    FeaturesComponent,
    LoginComponent,
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
  ],
})
export class HomeModule { }
