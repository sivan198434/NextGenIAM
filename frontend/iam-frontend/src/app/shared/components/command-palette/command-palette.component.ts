import { Component, HostListener, signal } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-command-palette',
  standalone: true,
  imports: [CommonModule],
  template: `
    @if (isOpen()) {
      <div class="fixed inset-0 z-50 flex items-start justify-center pt-24 sm:pt-40">
        <!-- Backdrop -->
        <div class="fixed inset-0 bg-slate-900/50 backdrop-blur-sm" (click)="close()"></div>

        <!-- Palette -->
        <div class="relative w-full max-w-xl bg-white rounded-xl shadow-2xl ring-1 ring-slate-900/5 overflow-hidden">
          <div class="p-4 border-b border-slate-100">
            <input type="text"
                   class="w-full text-lg text-slate-900 placeholder:text-slate-400 border-0 focus:ring-0"
                   placeholder="Search commands... (e.g. 'Create User', 'Audit Logs')"
                   autoFocus>
          </div>

          <div class="max-h-96 overflow-y-auto p-2">
            <div class="text-xs font-semibold text-slate-500 px-3 py-2 uppercase">Actions</div>
            @for (cmd of commands; track cmd.id) {
              <button class="w-full flex items-center px-3 py-2 text-sm text-slate-700 hover:bg-indigo-600 hover:text-white rounded-md transition-colors group">
                <span class="flex-1 text-left">{{cmd.label}}</span>
                <span class="text-xs text-slate-400 group-hover:text-indigo-200">{{cmd.shortcut}}</span>
              </button>
            }
          </div>

          <div class="bg-slate-50 px-4 py-3 text-xs text-slate-500 flex justify-between">
            <span>Press <kbd class="font-sans font-semibold">ESC</kbd> to close</span>
            <span>Navigate with <kbd class="font-sans font-semibold">↑↓</kbd></span>
          </div>
        </div>
      </div>
    }
  `,
  styles: [`
    :host { display: block; }
  `]
})
export class CommandPaletteComponent {
  isOpen = signal(false);

  commands = [
    { id: '1', label: 'Create New Identity', shortcut: '⌘N' },
    { id: '2', label: 'View Audit Logs', shortcut: '⌘L' },
    { id: '3', label: 'Manage Workflows', shortcut: '⌘W' },
    { id: '4', label: 'Identity Certification', shortcut: '⌘C' },
    { id: '5', label: 'System Health', shortcut: '⌘H' }
  ];

  @HostListener('window:keydown', ['$event'])
  handleKeyDown(event: KeyboardEvent) {
    if ((event.metaKey || event.ctrlKey) && event.key === 'k') {
      event.preventDefault();
      this.isOpen.set(!this.isOpen());
    }

    if (event.key === 'Escape') {
      this.isOpen.set(false);
    }
  }

  close() {
    this.isOpen.set(false);
  }
}
