using Mareen.DAL.IRepositories;
using Mareen.Domain.Entities;
using Mareen.Service.DTOs.Attachments;
using Mareen.Service.Exceptions;
using Mareen.Service.Extentions;
using Mareen.Service.Helpers;
using Mareen.Service.Interfaces;

namespace Mareen.Service.Services;

public class AttachmentService : IAttachmentService
{
    private readonly IRepository<Attachment> repository;
    public AttachmentService(IRepository<Attachment> repository)
    {
        this.repository = repository;
    }

    public async Task<Attachment> UploadAsync(string dirName, AttachmentCreationDto dto)
    {
        var webrootPath = Path.Combine(PathHelper.WebRootPath, dirName);

        if (!Directory.Exists(webrootPath))
            Directory.CreateDirectory(webrootPath);

        var fileExtension = Path.GetExtension(dto.FormFile.FileName);
        var fileName = $"{Guid.NewGuid().ToString("N")}{fileExtension}";
        var fullPath = Path.Combine(webrootPath, fileName);

        var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate);
        await fileStream.WriteAsync(dto.FormFile.ToByte());

        var createdAttachment = new Attachment
        {
            FileName = fileName,
            FilePath = fullPath
        };
        await this.repository.InsertAsync(createdAttachment);
        await this.repository.SaveAsync();

        return createdAttachment;
    }

    public async Task<Attachment> ModifyAsync(string dirName, long attachmentId, AttachmentCreationDto dto)
    {
        var attachment = await repository.SelectNoFilterAsync(x => x.Id.Equals(attachmentId))
            ?? throw new NotFoundException("Attachment was not found!");

        File.Delete(attachment.FilePath);

        var webrootPath = Path.Combine(PathHelper.WebRootPath, dirName);

        if (!Directory.Exists(webrootPath))
            Directory.CreateDirectory(webrootPath);

        var fileExtension = Path.GetExtension(dto.FormFile.FileName);
        var fileName = $"{Guid.NewGuid().ToString("N")}{fileExtension}";
        var fullPath = Path.Combine(webrootPath, fileName);

        var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate);
        await fileStream.WriteAsync(dto.FormFile.ToByte());

        attachment.FileName = fileName;
        attachment.FilePath = fullPath;

        this.repository.Update(attachment);
        await this.repository.SaveAsync();

        return attachment;
    }

    public async Task<bool> RemoveAsync(long attachmentId)
    {
        var attachment = await repository.SelectNoFilterAsync(x => x.Id.Equals(attachmentId))
            ?? throw new NotFoundException("Attachment was not found!");

        File.Delete(attachment.FilePath);

        this.repository.Drop(attachment);
        await this.repository.SaveAsync();

        return true;
    }
}
