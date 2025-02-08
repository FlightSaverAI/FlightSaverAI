import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'formatTime',
  standalone: true,
})
export class FormatTimePipe implements PipeTransform {
  transform(timeString: any): unknown {
    if (!timeString) return '';

    const timeParts = timeString.split(':');

    if (timeParts.length !== 3) {
      return 'Invalid format';
    }

    let hours = parseInt(timeParts[0], 10);

    const days = Math.floor(hours / 24);
    hours = hours % 24;

    return `${days} Days ${hours} Hours`.trim();
  }
}
