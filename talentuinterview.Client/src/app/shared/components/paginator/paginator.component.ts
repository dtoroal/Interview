import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'paginator',
  templateUrl: './paginator.component.html',
  styleUrl: './paginator.component.scss'
})
export class PaginatorComponent {
  @Input() currentPage!: number;
  @Input() itemsPerPage!: number;
  @Input() totalItems?: number;
  @Output() pageChanged: EventEmitter<number> = new EventEmitter();

  get totalPages(): number {
    return Math.ceil((this.totalItems ?? 0) / this.itemsPerPage);
  }

  public changePage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.pageChanged.emit(page);
    }
  }

}

