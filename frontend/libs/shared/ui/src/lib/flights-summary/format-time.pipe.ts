import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'formatTime',
  standalone: true,
})
export class FormatTimePipe implements PipeTransform {
  transform(value: any): unknown {
    if (!value) return '';

    const [daysPart, timePart] = value.split('.');
    const days = parseInt(daysPart, 10);
    const [hours, minutes] = timePart.split(':').map(Number);
    const totalHours = days * 24 + hours;

    return `${totalHours} h ${minutes} min`;
  }
}
