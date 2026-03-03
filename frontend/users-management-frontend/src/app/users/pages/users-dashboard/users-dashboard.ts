import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersService } from '../../services/users.service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-users-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './users-dashboard.html',
})
export class UsersDashboard implements OnInit {

  users: any[] = [];
  loading = false;
  error = '';

  constructor(private usersService: UsersService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.loading = true;
    this.error = '';

    this.usersService.getAll().subscribe({
      next: (data) => {
        this.users = data;
        console.log(this.users)
        this.loading = false;
      },
      error: () => {
        this.error = 'Error cargando usuarios';
        this.loading = false;
      }
    });
  }

  delete(id: string): void {
    if (!confirm('¿Desea eliminar este usuario?')) return;

    this.usersService.delete(id).subscribe({
      next: () => this.loadUsers(),
      error: () => this.error = 'Error eliminando usuario'
    });
  }
}
