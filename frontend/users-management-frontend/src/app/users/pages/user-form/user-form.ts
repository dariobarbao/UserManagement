import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UsersService } from '../../services/users.service';
import { UserRequest } from '../../models/user-request.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './user-form.html',
})
export class UserForm implements OnInit {
  form!: FormGroup;
  isEditMode = false;
  userId: string | null = null;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private usersService: UsersService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    // Obtener el ID de la ruta si existe
    this.userId = this.route.snapshot.paramMap.get('id');
    this.isEditMode = !!this.userId;

    this.initForm();

    if (this.isEditMode && this.userId) {
      this.loadUser(this.userId);
    }
  }

  private initForm(): void {
    // En modo edición, el password es opcional. En creación, es requerido.
    const passwordValidators = this.isEditMode
      ? [Validators.minLength(6)]
      : [Validators.required, Validators.minLength(6)];

    this.form = this.fb.group({
      firstName: ['', [Validators.required, Validators.minLength(2)]],
      lastName: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', passwordValidators],
      phoneNumber: [''],
      dateOfBirth: ['', Validators.required],
      isActive: [true]
    });
  }

  private loadUser(id: string): void {
    this.loading = true;
    this.usersService.getById(id).subscribe({
      next: (user) => {
        console.log(user)
        const dateOfBirth = user.dateOfBirth
        ? new Date(user.dateOfBirth).toISOString().slice(0, 10)
        : '';

        this.form.patchValue({
          firstName: user.firstName,
          lastName: user.lastName,
          email: user.email,
          phoneNumber: user.phoneNumber || '',
          dateOfBirth: dateOfBirth,
          isActive: user.isActive
          // No cargamos password por seguridad
        });
        this.loading = false;
      },
      error: (error) => {
        console.error('Error cargando usuario:', error);
        alert('Error al cargar el usuario');
        this.loading = false;
        this.router.navigate(['/users']);
      }
    });
  }

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading = true;
    const formValue = this.form.value;

    // Construir el objeto de request
    const userRequest: any = {
      firstName: formValue.firstName,
      lastName: formValue.lastName,
      email: formValue.email,
      phoneNumber: formValue.phoneNumber,
      dateOfBirth: formValue.dateOfBirth,
      isActive: formValue.isActive
    };

    // Solo incluir password si tiene valor (en creación siempre, en edición solo si se ingresó)
    if (formValue.password && formValue.password.trim() !== '') {
      userRequest.password = formValue.password;
    }

    const operation = this.isEditMode && this.userId
      ? this.usersService.update(this.userId, userRequest)
      : this.usersService.create(userRequest);

    operation.subscribe({
      next: () => {
        alert(this.isEditMode ? 'Usuario actualizado exitosamente' : 'Usuario creado exitosamente');
        this.router.navigate(['/users']);
      },
      error: (error) => {
        console.error('Error guardando usuario:', error);

        alert('Error al guardar el usuario');
        this.loading = false;
      }
    });
  }

  cancel(): void {
    this.router.navigate(['/users']);
  }
}
