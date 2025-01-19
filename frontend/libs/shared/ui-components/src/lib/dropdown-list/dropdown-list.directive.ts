import {
  ComponentRef,
  Directive,
  ElementRef,
  HostListener,
  inject,
  input,
  output,
} from '@angular/core';
import { DropdownListComponent } from './dropdown-list.component';
import { ComponentPortal } from '@angular/cdk/portal';
import { Overlay, OverlayRef } from '@angular/cdk/overlay';
import { Observable } from 'rxjs';

@Directive({
  selector: '[sharedDropdownList]',
  standalone: true,
})
export class DropdownListDirective {
  dropdownList = input<any>();
  isOpen = input<boolean>();
  isOpenChange = output<boolean>();
  selectedItem = output<any>();

  private _componentRef!: ComponentRef<DropdownListComponent> | null;
  private _overlayRef!: OverlayRef | null;
  private _elementRef = inject(ElementRef);
  private _overlay = inject(Overlay);

  @HostListener('click', ['$event'])
  protected toggleDropdown(event: Event): void {
    event.stopPropagation();

    if (!this._componentRef) {
      this._openOverlay();
    }
  }

  private _openOverlay(): void {
    const positionStrategy = this._overlay
      .position()
      .flexibleConnectedTo(this._elementRef)
      .withPositions([
        {
          originX: 'start',
          originY: 'bottom',
          overlayX: 'start',
          overlayY: 'top',
        },
      ]);

    if (!this._overlayRef) {
      this.isOpenChange.emit(!this.isOpen());
    }

    this._overlayRef = this._overlay.create({ positionStrategy });

    const portal = new ComponentPortal(DropdownListComponent);
    const componentRef = this._overlayRef.attach(portal);

    componentRef.instance.selectedItem.subscribe((selectedItem) => {
      this.selectedItem.emit(selectedItem);
      this._closeOverlay();
    });

    componentRef?.instance.clickOutside.subscribe(() => {
      this._closeOverlay();
    });

    this._componentRef = componentRef;

    if (this.dropdownList() instanceof Observable) {
      this.dropdownList().subscribe(<T>(list: T[]) => {
        componentRef.instance.filterProps = list;
      });
      return;
    }

    if (this.dropdownList() instanceof Array) {
      componentRef.instance.filterProps = this.dropdownList();
    }
  }

  private _closeOverlay(): void {
    if (this._overlayRef) {
      this.isOpenChange.emit(!this.isOpen());

      this._overlayRef.detach();
      this._componentRef?.destroy();

      this._overlayRef = null;
      this._componentRef = null;
    }
  }
}
