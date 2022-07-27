using Ardalis.GuardClauses;
using Notebook.Core.ProjectAggregate.Events;
using Notebook.SharedKernel;
using Notebook.SharedKernel.Interfaces;

namespace Notebook.Core.ProjectAggregate
{
    public class Project : EntityBase, IAggregateRoot
    {
        public string Name { get; private set; }

        private List<ToDoItem> _items = new List<ToDoItem>();
        public IEnumerable<ToDoItem> Items => _items.AsReadOnly();

        private List<Note> _notes = new List<Note>();
        public IEnumerable<Note> Notes => _notes.AsReadOnly();

        public ProjectStatus Status => _items.All(i => i.IsDone) ? ProjectStatus.Complete : ProjectStatus.InProgress;

        public PriorityStatus Priority { get; }

        public Project(string name, PriorityStatus priority)
        {
            Name = Guard.Against.NullOrEmpty(name, nameof(name));
            Priority = priority;
        }

        public void AddToDoItem(ToDoItem newToDoItem)
        {
            Guard.Against.Null(newToDoItem, nameof(newToDoItem));
            _items.Add(newToDoItem);

            var newItemAddedEvent = new NewToDoItemAddedEvent(this, newToDoItem);
            base.RegisterDomainEvent(newItemAddedEvent);
        }

        public void RemoveToDoItem(int toDoItemId)
        {
          var itemToRemove = _items.FirstOrDefault(x => x.Id == toDoItemId);

          if (itemToRemove == null)
          {
            throw new ArgumentException($"ToDoItem with ID: {toDoItemId} not exists.");
          }

          _items.Remove(itemToRemove);

          var newItemRemovedEvent = new ToDoItemRemovedEvent(this, toDoItemId);
          base.RegisterDomainEvent(newItemRemovedEvent);
        }

        public void AddNote(Note newNote)
        {
          Guard.Against.Null(newNote, nameof(newNote));
          _notes.Add(newNote);

          var newNoteAddedEvent = new NewNoteAddedEvent(this, newNote);
          base.RegisterDomainEvent(newNoteAddedEvent);
        }

        public void RemoveNote(int noteId)
        {
          var noteToRemove = _notes.FirstOrDefault(x => x.Id == noteId);

          if (noteToRemove == null)
          {
            throw new ArgumentException($"Note with ID: {noteId} not exists.");
          }

          _notes.Remove(noteToRemove);

          var newNoteRemovedEvent = new NoteRemovedEvent(this, noteId);
          base.RegisterDomainEvent(newNoteRemovedEvent);
        }

        public void UpdateName(string newName)
            {
                Name = Guard.Against.NullOrEmpty(newName, nameof(newName));
            }
        }
}
