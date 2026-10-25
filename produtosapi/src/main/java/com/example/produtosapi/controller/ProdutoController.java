package com.example.produtosapi.controller;

import com.example.produtosapi.model.Produto;
import com.example.produtosapi.repository.ProdutosRepository;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.UUID;

@RestController
@RequestMapping("produtos")
public class ProdutoController {
    private final ProdutosRepository produtoRepository;

    public ProdutoController(ProdutosRepository produtosRepository) {
        produtoRepository = produtosRepository;
    }

    @PostMapping
    public Produto Salvar(@RequestBody Produto produto) {
        produto.setId(UUID.randomUUID().toString());

        produtoRepository.save(produto);

        return produto;
    }

    @GetMapping("{id}")
    public Produto BuscarPorId(@PathVariable String id) {
        return produtoRepository.findById(id).orElse(null);
    }

    @DeleteMapping("{id}")
    public void DeleteById(@PathVariable String id) {
        produtoRepository.deleteById(id);
    }

    @PutMapping("{id}")
    public void Atualizar(@PathVariable("id") String id,
                                 @RequestBody Produto produto) {
         produto.setId(id);
         produtoRepository.save(produto);
    }

    @GetMapping
    public List<Produto> Buscar(@RequestParam String nome) {
        return produtoRepository.findByNome(nome);
    }
}
