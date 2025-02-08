import { ConnectedPosition, Overlay, OverlayRef } from '@angular/cdk/overlay';
import { ComponentPortal } from '@angular/cdk/portal';
import { Directive, ElementRef, HostListener, inject, input, output } from '@angular/core';
import { DropdownComponent } from './dropdown.component';

@Directive({
  selector: '[sharedDropdown]',
  standalone: true,
})
export class DropdownDirective {
  dropdownConfig = input.required<any>();
  selectOption = output<string>();

  positions: ConnectedPosition[] = [
    {
      originX: 'end',
      originY: 'bottom',
      overlayX: 'end',
      overlayY: 'top',
      offsetX: 25,
      offsetY: 10,
    },
  ];

  private _overlayRef: OverlayRef | null = null;

  private _overlay = inject(Overlay);
  private _elementRef = inject(ElementRef);

  @HostListener('click')
  toggleDropdown() {
    if (!this._overlayRef) {
      const positionStrategy = this._overlay
        .position()
        .flexibleConnectedTo(this._elementRef)
        .withPositions(this.positions);

      this._overlayRef = this._overlay.create({ positionStrategy });

      const portal = new ComponentPortal(DropdownComponent);
      const componentRef = this._overlayRef.attach(portal);

      componentRef.instance.dropdownConfig = this.dropdownConfig();
      componentRef.instance.selectOption.subscribe((url) => this.selectOption.emit(url));
    }
  }

  @HostListener('document:click', ['$event.target'])
  onOutsideClick(target: HTMLElement) {
    if (this._overlayRef && !this._elementRef.nativeElement.contains(target)) {
      this._overlayRef.detach();
      this._overlayRef = null;
    }
  }
}
