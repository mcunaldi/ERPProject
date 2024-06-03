import { Component } from '@angular/core';
import { SharedModule } from '../../modules/shared.module';

@Component({
  selector: 'app-depo',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './depo.component.html',
  styleUrl: './depo.component.css'
})
export class DepoComponent {

}
